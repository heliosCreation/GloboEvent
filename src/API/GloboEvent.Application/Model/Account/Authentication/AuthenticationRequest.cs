using System.ComponentModel.DataAnnotations;

namespace GloboEvent.Application.Model.Account.Authentification
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
