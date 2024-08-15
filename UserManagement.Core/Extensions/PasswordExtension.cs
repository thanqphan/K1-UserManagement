using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Extensions
{
    public static class PasswordExtension
    {
        public static string HashPassword(this string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltPassword = $"{salt}{password}";
                var bytes= sha256.ComputeHash(Encoding.UTF8.GetBytes(saltPassword));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(this string hashedPassword, string providedPassword, string salt)
        {
            var hashOfProvidedPassword = providedPassword.HashPassword(salt);
            return hashedPassword == hashOfProvidedPassword;
        }
    }
}
