using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace DemoProject.Core.Models
{
    public record UserModel
    {
        [Key]
        public Guid id { get; init; }
        
        [Required, Range(8,50)]
        public MailAddress email { get; init; }

        [Required, Range(3, 32)]
        public string username { get; init; }

        [Required]
        public IdentityRole role { get; init; }
    }
}
