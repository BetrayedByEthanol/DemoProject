using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace DemoProject.Core.Models
{
    public record UserModel
    {
        [Key]
        public Guid id { get; init; }

        [Required, MinLength(8), MaxLength(50)]
        public MailAddress Email { get; init; }

        [Required, MinLength(3), MaxLength(32)]
        public string UserName { get; init; }

        [Required]
        public IdentityRole Role { get; init; }
    }
}
