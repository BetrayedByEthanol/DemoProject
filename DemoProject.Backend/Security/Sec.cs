using DemoProject.Backend.Data;
using DemoProject.Backend.ModelDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Backend.Security
{
    class Sec : IPasswordHasher<UserIdentity>
    {

        public string HashPassword(UserIdentity user, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 14, BCrypt.Net.HashType.SHA512);
        }

        public PasswordVerificationResult VerifyHashedPassword(UserIdentity user, string hashedPassword, string providedPassword)
        {
            if (BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword, BCrypt.Net.HashType.SHA512)) return PasswordVerificationResult.Success;
            else return PasswordVerificationResult.Failed;
        }
    }
}
