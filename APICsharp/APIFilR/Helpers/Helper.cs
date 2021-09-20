using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace APIFilR.Helpers
{
    public static class Helper
    {
        public static string ConVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public static bool VerifyHash(string password, string hash)
        {
            return Argon2.Verify(hash, password);
        }
        public static string GetPathImage()
        {
            return ConfigurationManager.AppSettings["PathImages"];
        }
    }
}
