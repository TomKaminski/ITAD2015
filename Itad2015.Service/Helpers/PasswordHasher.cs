using System;
using System.Security.Cryptography;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Service.Helpers
{
    public class PasswordHasher: IPasswordHasher
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int Pbkdf2Iterations = 1000;

        public string CreateHash(string password)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = Pbkdf2(password, salt,Pbkdf2Iterations, HashByteSize);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }


        public bool ValidatePassword(string password, string correctHash, string correctSalt)
        {
            var salt = Convert.FromBase64String(correctSalt);
            var hash = Convert.FromBase64String(correctHash);
            var testHash = Pbkdf2(password, salt,Pbkdf2Iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        public string GetSalt(string hashedPassword)
        {
            char[] delimiter = { ':' };
            var split = hashedPassword.Split(delimiter);
            return split[0];
        }

        public string GetHash(string hashedPassword)
        {
            char[] delimiter = { ':' };
            var split = hashedPassword.Split(delimiter);
            return split[1];
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] Pbkdf2(string password, byte[] salt,int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}