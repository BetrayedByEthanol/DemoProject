using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DemoProject.Backend.Security
{
    class LookUpNormalizer : ILookupNormalizer
    {
        public string NormalizeEmail(string email)
        {
            return email;
        }

        public string NormalizeName(string name)
        {
            return name;
        }
    }
}
