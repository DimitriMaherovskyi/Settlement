using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Settlement.Web.Code
{
    public class MD5CryptoProvider
    {
        public static string ComputeHash(string input)
        {
            var md5 = MD5CryptoServiceProvider.Create();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input));

            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                // Change hash into 2 hexadecimal digits
                // for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}