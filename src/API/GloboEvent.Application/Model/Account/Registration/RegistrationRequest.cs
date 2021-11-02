using System.ComponentModel.DataAnnotations;

namespace GloboEvent.Application.Model.Account.Registration
{
    public class RegistrationRequest
    {
        [Required]
        [MaxLength(120)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(120)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(120)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(120)]
        public string Password { get; set; }
    }
}
