using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace log
{
    class Program
    {
        static void Main(string[] args)
        {

            var a1 = new HashSet<int>( new int[] {1,2,3,4,5});
            var a2 = new HashSet<int>( new int[] {4,5,6,7,8});

            //4,5  Общие файлы
            //a1.IntersectWith(a2);

            //1,2,3 эксклюзивные файлы
            //a1.ExceptWith(a2);

            //1,2,3,6,7,8 - всё кроме общих файлов
            //a1.SymmetricExceptWith(a2);

            return;


          var f1 = new Master(@"f:\_TEST\ver1",@"_log_test\ver1\");
          var f2 = new Master(@"f:\_TEST\ver2",@"_log_test\ver2\");
          f1.MakePair(f2);
          f1.Save();
          f2.Save();

          return;

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
          foreach (var item in f1.exclusiveFiles)
          {
              Console.WriteLine(item);
          }

          Console.WriteLine("f2 miss");
          foreach (var item in f2.exclusiveFiles)
          {
              Console.WriteLine(item);
          }


          //var fi = new fileInfo("rel",DateTime.Now, 1001);

          var s = JsonConvert.SerializeObject(f1);

          var fi2 = JsonConvert.DeserializeObject<FileInformation>(s);

          Console.WriteLine(s);
                 Console.WriteLine(fi2);

        }
    }
}
