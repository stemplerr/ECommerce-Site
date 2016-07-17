﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public static class PasswordHelper
    {
        public static  string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[10];
            provider.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        public static string HashPassword(string password, string salt)
        {
            SHA256Managed crypt = new SHA256Managed();
            string combinedString = password + salt;
            byte[] combined = Encoding.Unicode.GetBytes(combinedString);
            byte[] hash = crypt.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }
        public static bool isMatch(string passwordAttempt, string hashedPassword, string salt)
        {
            return HashPassword(passwordAttempt, salt) == hashedPassword;
        }
    }
}
