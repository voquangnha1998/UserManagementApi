using System.Security.Cryptography;
using System.Text;

namespace UserApi.Utils
{
    public static class PasswordHasher
    {
        public static string Hash(string password) =>
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

        public static bool Verify(string password, string hash) =>
            Hash(password) == hash;
    }
}
