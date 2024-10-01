using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppMain
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } = null;
        public bool Aktif { get; set; }


       public static  string HashPasword(string password)
        {

            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return passwordHash;
        }


        public static bool VerifyPassword(string password, string passwordhash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordhash);
        }



    }
}
