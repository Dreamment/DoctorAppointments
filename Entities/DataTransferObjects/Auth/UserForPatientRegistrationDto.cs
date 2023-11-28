using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Auth
{
    public class UserForPatientRegistrationDto
    {
        [Required]
        public string Password { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string PhoneNumber { get; init; }
    }
}
