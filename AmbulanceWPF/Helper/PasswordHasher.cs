using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AmbulanceWPF.Helper
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;      // 128 bit
        private const int HashSize = 32;      // 256 bit
        private const int Iterations = 100_000;

        public static string Hash(SecureString password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password.ToUnmanagedString(), salt, Iterations, HashAlgorithmName.SHA256);

            var hash = pbkdf2.GetBytes(HashSize);

            var result = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

            return Convert.ToBase64String(result);
        }

        public static bool Verify(SecureString password, string storedHash)
        {
            var combined = Convert.FromBase64String(storedHash);
            if (combined.Length < SaltSize + HashSize) return false;

            var salt = new byte[SaltSize];
            var hash = new byte[HashSize];
            Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(combined, SaltSize, hash, 0, HashSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password.ToUnmanagedString(), salt, Iterations, HashAlgorithmName.SHA256);

            var test = pbkdf2.GetBytes(HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, test);
        }
    }
    public static class SecureStringExtensions
    {
        public static string ToUnmanagedString(this SecureString secureString)
        {
            if (secureString == null || secureString.Length == 0)
                return string.Empty;

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToCoTaskMemUnicode(secureString);
                return Marshal.PtrToStringUni(ptr) ?? string.Empty;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.ZeroFreeCoTaskMemUnicode(ptr);
            }
        }
    }
}
