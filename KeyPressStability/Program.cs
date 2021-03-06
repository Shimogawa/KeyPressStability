﻿using System;
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
     3. Run in background (maybe), record keys.
     */
    class Program
    {
        static int Main(string[] args)
        {
            bool goon;
            var proc = new KPSMain(out goon);

            if (args.Length > 0)
            {
                proc.ShowHelp(false);
                return 0;
            }

            if (goon)
                proc.Start();

            return 0;
        }
    }
}
