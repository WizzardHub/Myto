using System;
using System.Linq;

namespace KeyGen.Extensions
{
    public static class StringExt
    {
        public static string WithDelimiter(this string self, double length, string divider = "-")
        {
            return String.Join(divider, Enumerable.Range(0, (int) Math.Ceiling(self.Length / length))
                .Select(i => new string(self
                    .Skip(i * (int) length)
                    .Take((int) length)
                    .ToArray())));
        }
    }
}