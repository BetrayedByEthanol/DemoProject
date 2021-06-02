using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public record RegisterModel : LoginModel
    {
        public MailAddress Email { get; init; }
    }
}
