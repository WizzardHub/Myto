using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KeyGen.Utils
{
    public class FileUtils
    {
        public static async Task SaveKeys(string keys)
        {
            var currentCheckPath = Process
                .GetCurrentProcess()
                .StartTime
                .ToString("R")
                .Replace(":", "-");

            if (!Directory.Exists($"Results\\{currentCheckPath}"))
                Directory.CreateDirectory($"Results\\{currentCheckPath}");

            using (var outputFile = new StreamWriter($"Results\\{currentCheckPath}\\mw-serials.txt", true))
            {
                Exception:
                try
                {
                    await outputFile
                        .WriteLineAsync(keys)
                        .ConfigureAwait(false);
                }
                catch
                {
                    goto Exception;
                }
            }
        }
    }
}