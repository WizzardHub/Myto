using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeyGen.Extensions;
using KeyGen.Utils;

namespace KeyGen
{
    internal class Myto
    {

        private const int Stack = 1000;
        private static Random Random { get; }
            = new Random();

        private static string Keys { get; set; } = "";
        
        public static void Main(string[] args)
        {
            var amount = int.Parse(Console.ReadLine());
            var loop = Enumerable
                .Range(0, Math.Max(1, amount))
                .ToArray()
                .Split(Stack);

            loop
                .ToList()
                .ForEach(async _ =>
                {
                    var key = new string[_.Count()];
                    for (var i = 0; i < key.Length; i++)
                        key[i] = await GenSerialKey();

                    var keys = String.Join("\n", key);

                    Exception:
                    try
                    {
                        await FileUtils.SaveKeys(keys);
                    }
                    catch
                    {
                        goto Exception;
                    }
                });
            
            Console.WriteLine("Generated");
            Thread.Sleep(-1);
        }

        private const string Nums = "0123456789";
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Alpha = "0123456789ABCDEFGHJKLMNPQRTUVWXY";
        
        private static async Task<string> GenSerialKey()
        {
            var id = $"{Nums[Random.Next(0, 9)]}{Letters[Random.Next(0, 26)]}{Letters[Random.Next(0, 26)]}{Nums[Random.Next(0, 9)]}{Nums[Random.Next(0, 9)]}";
            using (var md5 = MD5.Create())
            {
                var hash = md5.HashString(id);
                var serial = string.Empty;
                for (var p = 0; p < 0x20; p += 2)
                    serial += Convert.ToString(Alpha[Convert.ToChar((long)((Convert.ToInt32(hash.Substring(p, 2), 0x10) & 0x8000001fL) + 1L)) - '\x0001']);
                return $"{id}:{serial}";
            }
        }
    }
}