using GloboEvent.Application.Contracts.Identity;
using GloboEvent.Application.Contrats.Infrastructure;
using GloboEvent.Application.Model.Authentification;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace GloboEvent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public AccountController(IAuthenticationService authenticationService, IEmailService emailService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequest request)
        {
            var response = await _authenticationService.RegisterAsync(request);
            if (response.Succeeded)
            {
                var code = await _authenticationService.GenerateRegistrationEncodedToken(response.Data.UserId);
                var callbackLink = Url.ActionLink("ConfirmEmail", "Account", new { Email = request.Email, code = code });

                await _emailService.SendRegistrationMail(request.Email, callbackLink);
            }
            return Ok(response);
        }

        [Route("ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var response = await _authenticationService.ConfirmEmail(email, code);
            return Ok(response);
        }
    }
}
