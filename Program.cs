using System;
using System.Collections.Generic;
using System.Linq;

namespace log
{
    class Program
    {
        static void Main(string[] args)
        {
          var f1 = new File(@"e:\Games\_log_test\ver1\");
          var f2 = new File(@"e:\Games\_log_test\ver2\");

          Console.WriteLine("before");
          foreach (var item in f1.files)
          {
              Console.WriteLine(item);
          }

         // f1.files.ExceptWith(f2.files);

        var f = f1.GetMissingFiles(f2);

         // var hs = new HashSet<fileInfo>();

        
         // foreach (var item in f) hs.Add(item);

            Console.WriteLine("after");
          foreach (var item in f)
          {
              Console.WriteLine(item);
          }
        }
    }
}
