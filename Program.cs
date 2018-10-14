using System;
using System.Linq;

namespace log
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = System.IO.File.ReadAllLines(@"e:\Games\Eagle Dynamics\Logfile.CSV.txt")
            .Where(f=>f.Contains("\",\"Reg") && f.ToLower().Contains("write") );

            System.IO.File.WriteAllLines(@"e:\Games\Eagle Dynamics\Logfile.CSV.txt.txt",o);
     
            Console.WriteLine("Hello World!");
        }
    }
}
