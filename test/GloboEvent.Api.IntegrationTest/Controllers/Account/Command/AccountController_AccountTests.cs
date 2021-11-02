using FluentAssertions;
using GloboEvent.Api.IntegrationTest.Base;
using GloboEvent.API.Contract;
using GloboEvent.Application.Model.Account.Authentification;
using GloboEvent.Application.Model.Account.Registration;
using GloboEvent.Application.Responses;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GloboEvent.Api.IntegrationTest.Controllers.Account.Command
{
    using static ApiRoutes.Account;
    using static Utils.AccountTools;

    public class AccountController_AccountTests : IntegrationTestBase
    {
        #region Login
        [Fact]
        public async Task Login_willReturns_CorrectStatusCodeAndToken_WhenValidCredentialsArePassed()
        {

            var response = await TestClient.PostAsJsonAsync(Authenticate,
                new AuthenticationRequest
                {
                    Email = "john@gmail.com",
                    Password = "Pwd12345!"
                });
            var content = await response.Content.ReadAsAsync<ApiResponse<AuthenticationResponse>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Should().NotBeNull();
            content.Data.Token.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_willReturns_401StatusCodeAndNoData_WhenUnregisteredCredentialsArePassed()
        {

            var response = await TestClient.PostAsJsonAsync(Authenticate,
                new AuthenticationRequest
                {
                    Email = "unknown@gmail.com",
                    Password = "Pwd12345!"
                });
            var content = await response.Content.ReadAsAsync<ApiResponse<AuthenticationResponse>>();

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            content.Data.Should().BeNull();
        }

        [Fact]
        public async Task Login_willFail_ifEmailIsNotConfirmed()
        {
            await TestClient.PostAsJsonAsync(Register,
                new RegistrationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!",
                    FirstName = "test",
                    LastName = "test",
                    UserName = "test"
                });

            var response = await TestClient.PostAsJsonAsync(Authenticate,
                new AuthenticationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!"
                });
            var content = await response.Content.ReadAsAsync<ApiResponse<AuthenticationResponse>>();

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            content.Data.Should().BeNull();
        }
        #endregion

        #region Register
        [Fact]
        public async Task Register_willReturns_CorrectStatusCodeAndData_WhenValidCredentialsArePassed()
        {
            var response = await TestClient.PostAsJsonAsync(Register,
                new RegistrationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!",
                    FirstName="test",
                    LastName = "test",
                    UserName = "test"
                });
            var content = await response.Content.ReadAsAsync<ApiResponse<RegistrationResponse>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Data.Should().NotBeNull();
            content.Data.UserId.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(RegisterValidationTest))]
        public async Task Register_willReturns_BadRequest_WhenInValidFieldsArePassed(string firstname, string lastname, string username, string email, string password)
        {

            var response = await TestClient.PostAsJsonAsync(Register,
                new RegistrationRequest
                {
                    Email = email,
                    Password = password,
                    FirstName = firstname,
                    LastName = lastname,
                    UserName = username
                });

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
        #endregion

        #region EmailConfirmation
        [Fact]
        public async Task EmailConfirmation_returnsOKStatusCode()
        {
            var registrationResponse = await TestClient.PostAsJsonAsync(Register,
                new RegistrationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!",
                    FirstName = "test",
                    LastName = "test",
                    UserName = "test"
                });
            var registrationContent = await registrationResponse.Content.ReadAsAsync<ApiResponse<RegistrationResponse>>();
            var callBackUrl = registrationContent.Data.callbackURL;

            var response = await TestClient.GetAsync(callBackUrl);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task EmailConfirmation_enableUserLogin()
        {
            var registrationResponse = await TestClient.PostAsJsonAsync(Register,
                new RegistrationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!",
                    FirstName = "test",
                    LastName = "test",
                    UserName = "test"
                });
            var registrationContent = await registrationResponse.Content.ReadAsAsync<ApiResponse<RegistrationResponse>>();
            var callBackUrl = registrationContent.Data.callbackURL;

           await TestClient.GetAsync(callBackUrl);

            var response = await TestClient.PostAsJsonAsync(Authenticate,
                new AuthenticationRequest
                {
                    Email = "test@gmail.com",
                    Password = "Pwd12345!"
                });
            response.StatusCode.Should().Be(HttpStatusCode.OK);


        }
        #endregion
    }
}
