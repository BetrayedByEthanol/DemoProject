using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public record LoginModel
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
