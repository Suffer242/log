using System;
using System.Collections.Generic;
using System.Linq;

namespace log
{
    class Program
    {
        static void Main(string[] args)
        {
          var f1 = new File(@"_log_test\ver1\");
          var f2 = new File(@"_log_test\ver2\");

          Console.WriteLine("before f1");
          foreach (var item in f1.files)
          {
              Console.WriteLine(item);
          }

          Console.WriteLine("before f2");
          foreach (var item in f2.files)
          {
              Console.WriteLine(item);
          }




            Console.WriteLine("f1 miss");
          foreach (var item in f1.GetMissingFiles(f2))
          {
              Console.WriteLine(item);
          }

            Console.WriteLine("f2 miss");
          foreach (var item in f2.GetMissingFiles(f1))
          {
              Console.WriteLine(item);
          }

        }
    }
}
