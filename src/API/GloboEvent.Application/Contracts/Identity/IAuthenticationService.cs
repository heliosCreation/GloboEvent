using GloboEvent.Application.Model.Authentification;
using GloboEvent.Application.Responses;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
        Task<ApiResponse<object>> ConfirmEmail(string email, string token);
        Task<string> GenerateRegistrationEncodedToken(string id);
        Task<ApiResponse<RegistrationResponse>> RegisterAsync(RegistrationRequest request);
        Task<bool> UserExist(string email);
    }
}
