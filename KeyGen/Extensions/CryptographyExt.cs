using System.Security.Cryptography;
using System.Text;

namespace KeyGen.Extensions
{
    public static class CryptographyExt
    {
        public static string HashString(this MD5 self, string data) 
            => GetMd5Hash(self.ComputeHash(Encoding.UTF8.GetBytes(data)));
        
        private static string GetMd5Hash(byte[] data)
        {
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}