using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DemoProject.Core.Models
{
    record LoginModel
    {
        UserModel user { get; init; }
        SecureString password { get; init; }
    }
}
