using GloboEvent.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GloboEvent.Api.IntegrationTest.Base
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
               services.AddDbContext<GloboEventDbContext>(opt =>
               {
                   opt.UseInMemoryDatabase("GloboEventInMemoryTest");
               });

               var sp = services.BuildServiceProvider();
               using var scope = sp.CreateScope();
               var scopedServices = scope.ServiceProvider;
               var context = scopedServices.GetRequiredService<GloboEventDbContext>();
               var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

               context.Database.EnsureCreated();

               try
               {
                   Utilies.InitializeDatabaseForTest(context);
               }
               catch (Exception e)
               {

                   logger.LogError($"An error occured while seeding the database. Error : {e.Message}");
               }
           });
            base.ConfigureWebHost(builder);
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
