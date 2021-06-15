using DemoProject.Backend.ModelDTOs;
using DemoProject.Core.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DemoProject.Backend
{
    static class Mapping
    {
        public static void cofigureMappingTable()
        {
            TypeAdapterConfig<RegisterModel, UserIdentity>.NewConfig()
                   .Map(dest => dest.UserName, src => BCrypt.Net.BCrypt.HashPassword(src.UserName ?? "", generateSalt(src.UserName), true, BCrypt.Net.HashType.SHA512))
                   .Map(dest => dest.Email, src => BCrypt.Net.BCrypt.HashPassword(src.Email.Address ?? "", generateSalt(src.Email.Address), true, BCrypt.Net.HashType.SHA512))
                   .Map(dest => dest.PasswordHash, src => BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password, 14, BCrypt.Net.HashType.SHA512));
            TypeAdapterConfig<RegisterModel, UserModel>.NewConfig()
                .Map(dest => dest.Role, src => new IdentityRole("Developer"));

        }


        private static string generateSalt(string basis)
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
