using System.Net.Mail;

namespace DemoProject.Core.Models
{
    public record RegisterModel : LoginModel
    {
        public MailAddress Email { get; init; }
    }
}
