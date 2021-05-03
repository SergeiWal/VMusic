using System;
using System.Security.Cryptography;
using System.Text;

namespace VMusic.Hasher
{
    static class PasswordHasher
    {
        public static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
