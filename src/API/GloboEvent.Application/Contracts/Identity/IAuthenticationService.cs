using GloboEvent.Application.Model.Authentification;
using GloboEvent.Application.Responses;
using System.Threading.Tasks;

namespace GloboEvent.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
        Task<ApiResponse<RegistrationResponse>> RegisterAsync(RegistrationRequest request);
    }
}
