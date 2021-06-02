using DemoProject.Backend.Data;
using DemoProject.Backend.ModelDTOs;
using DemoProject.Core;
using DemoProject.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DemoProject.Backend
{
    public class DemoSetup
    {

        public DemoSetup(ApplicationDbContext dbContext, UserManager<UserIdentity> userManager)
        {

            if (dbContext.Users.Count() == 0)
            {
                RegisterModel user = new()
                {
                    UserName = "demo",
                    Email = new System.Net.Mail.MailAddress("demo@account.com"),
                    Password = "demo"
                };

                TypeAdapterConfig<RegisterModel, UserIdentity>.NewConfig()
                    .Map(dest => dest.UserName, src => BCrypt.Net.BCrypt.HashPassword(src.UserName ?? "", generateSalt(src.UserName), true, BCrypt.Net.HashType.SHA512))
                    .Map(dest => dest.Email, src => BCrypt.Net.BCrypt.HashPassword(src.Email.Address ?? "", generateSalt(src.Email.Address), true, BCrypt.Net.HashType.SHA512))
                    .Map(dest => dest.PasswordHash, src => BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password, 14, BCrypt.Net.HashType.SHA512));
                TypeAdapterConfig<RegisterModel, UserModel>.NewConfig()
                    .Map(dest => dest.Role, src => new IdentityRole("Developer"));


                UserModel userModel = user.Adapt<UserModel>();
                UserIdentity uid = user.Adapt<UserIdentity>();


                //userManager.CreateAsync(uid).GetAwaiter().GetResult();
                //dbContext.Users.Add(userModel);
                //dbContext.SaveChanges();

                bool isVerified = false;


                var hashedUsername = BCrypt.Net.BCrypt.HashPassword(user.UserName, generateSalt(user.UserName), true, BCrypt.Net.HashType.SHA512);
                var useridentity = userManager.FindByNameAsync(hashedUsername).GetAwaiter().GetResult();
                if (useridentity is not null && BCrypt.Net.BCrypt.EnhancedVerify(user.Password, useridentity.PasswordHash, BCrypt.Net.HashType.SHA512))
                {
                    isVerified = true;
                }

            }


        }

        private string generateSalt(string basis)
        {
            var allowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/";
            var salt = "";
            var bytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(basis));
            foreach (byte b in bytes) salt += allowed[((int)b) % allowed.Length];
            salt = "$2a$14$" + salt;
            return salt;
        }
    }
}
