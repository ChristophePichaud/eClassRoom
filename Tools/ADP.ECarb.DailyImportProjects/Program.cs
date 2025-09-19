using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADP.Ecarb.DailyImportProjects
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main... args count:{0}", args.Length);
            foreach( string arg in args)
            {
                Console.WriteLine("Arg:{0}", arg);
            }

            DailyImportProcess dip = new DailyImportProcess();
            await dip.DoWorkAsync();
        }
    }
}
