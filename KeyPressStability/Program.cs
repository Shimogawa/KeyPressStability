using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPressStability
{
    /*
     todo:
     1. seperate osu and rts game apm counter. Seperate in Main().
     2. put help docs into Main() and localize it.
     */
    class Program
    {
        static void Main(string[] args)
        {
            bool goon;
            var proc = new KPSMain(out goon);

            if (goon)
                proc.Start();
        }
    }
}
