using GloboEvent.API;
using GloboEvent.API.Contract;
using GloboEvent.Application.Features.Categories;
using GloboEvent.Application.Features.Categories.Commands.Create;
using GloboEvent.Application.Features.Events.Commands.CreateEvent;
using GloboEvent.Application.Model.Authentification;
using GloboEvent.Application.Responses;
using GloboEvent.Identity;
using GloboEvent.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GloboEvent.Api.IntegrationTest.Base
{
    using static ApiRoutes;
    public class IntegrationTestBase : IDisposable
    {
        protected readonly HttpClient TestClient;
        private IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()

                .WithWebHostBuilder(builder =>
                {
                    //Change the json file used by the integration tests
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        _configuration = new ConfigurationBuilder()
                         .AddJsonFile("integrationsettings.json")
                         .Build();

                        config.AddConfiguration(_configuration);
                    });

                    builder.ConfigureServices(services =>
                    {
                        //Remove the DbContext service from the original startup
                        services.RemoveAll(typeof(DbContextOptions<GloboEventDbContext>));
                        //Add our test db
                        var connectionString = _configuration.GetConnectionString("IntegrationData").Replace("{name}",Guid.NewGuid().ToString());
                        services.AddDbContext<GloboEventDbContext>(
                           opt => opt.UseSqlServer(connectionString,
                           b => b.MigrationsAssembly(typeof(GloboEventDbContext).Assembly.FullName))
   );

                        services.RemoveAll(typeof(GloboEventIdentityDbContext));
                        services.AddDbContext<GloboEventIdentityDbContext>(options =>
                        {
                            options.UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot());
                        });

                    });
                });



            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
            TestClient.BaseAddress = new Uri("https://localhost:5001");

            using var serviceScope = _serviceProvider.CreateScope();

            var dataContext = serviceScope.ServiceProvider.GetRequiredService<GloboEventDbContext>();
            dataContext.Database.Migrate();
            var res = dataContext.Database.EnsureCreated();
        }


        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<ApiResponse<CategoryVm>> CreateCategoryAsync(CreateCategoryCommand command)
        {
            var response = await TestClient.PostAsJsonAsync(Category.Create, command);
            return await response.Content.ReadAsAsync<ApiResponse<CategoryVm>>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(Account.Authenticate, new AuthenticationRequest
            {
                Email = "john@gmail.com",
                Password = "123Pa$$word!"
            });

            var content = await response.Content.ReadAsAsync<ApiResponse<AuthenticationResponse>>();
            return content.Data.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var dataContext = serviceScope.ServiceProvider.GetRequiredService<GloboEventDbContext>();

            dataContext.Database.EnsureDeleted();
        }
    }
}
