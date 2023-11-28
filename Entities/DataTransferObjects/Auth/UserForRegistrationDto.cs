﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Auth
{
    public class UserForRegistrationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
