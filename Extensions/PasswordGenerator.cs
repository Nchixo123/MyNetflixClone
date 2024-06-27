using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public static class PasswordGenerator
    {
        private static readonly char[] _chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()".ToCharArray();

        public static string Generate(int size = 12)
        {
            var data = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(_chars[b % _chars.Length]);
            }

            return result.ToString();
        }
    }
}
