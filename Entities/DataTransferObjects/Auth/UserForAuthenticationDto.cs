using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Auth
{
    public record UserForAuthenticationDto
    {
        [Required]
        public string Username { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
