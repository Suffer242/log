using System;
using System.Linq;

namespace log
{
    class Program
    {
        static void Main(string[] args)
        {
          var f1 = new File(@"e:\Games\Eagle Dynamics\DCS World\");
          var f2 = new File(@"g:\dcs\DCS World\");

          var f = f1.GetMissingFiles(f2);

          foreach (var item in f)
          {
              Console.WriteLine(item);
          }
        }
    }
}
