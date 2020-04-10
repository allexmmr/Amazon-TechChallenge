using System;
using System.Collections.Generic;
using System.Linq;

namespace LexicographicallySort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> logFile = new List<string>
            {
                "r1 a c a9",
                "a9 r1 a c",
                "1 a 2 b 3 c"
            };

            SortLogFile(2, logFile.ToArray());
        }

        public static List<string> SortLogFile(int fileSize, string[] logFile)
        {
            string[] arr = logFile.Take(fileSize).ToArray();

            Array.Sort(arr);

            return arr.ToList();
        }
    }
}