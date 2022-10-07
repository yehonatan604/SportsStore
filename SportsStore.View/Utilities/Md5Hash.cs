using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Utilities
{
    public class Md5Hash
    {
        public static string Create(string str)
        {
            // convert string into byte[] & encode it
            byte[] data = new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(str));
            string hash = Encoding.ASCII.GetString(data);

            // calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(hash);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // convert byte[] to hex string
            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
