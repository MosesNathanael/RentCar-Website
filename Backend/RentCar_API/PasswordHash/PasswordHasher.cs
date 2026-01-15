using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using RentCar_API.Models;

namespace RentCar_API.PasswordHash
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iteration = 10000;
        private static readonly HashAlgorithmName Algo = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration, Algo, HashSize);

            return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
        }

       public bool checkPassword(string hashedPassword, string passwordinput)
        {
            var arr = hashedPassword.Split('-');
            Console.WriteLine(arr);
            if (arr.Length != 2) return false;

            byte[] salt = Convert.FromHexString(arr[0]);
            byte[] storedHash = Convert.FromHexString(arr[1]);

            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(passwordinput, salt, Iteration, Algo, HashSize);
            int idx = 0;
            foreach(var i in storedHash)
            {
                if(i != computedHash[idx])
                {
                    return false;
                }
                idx++;
            }
            return true;
        }

    }
}
