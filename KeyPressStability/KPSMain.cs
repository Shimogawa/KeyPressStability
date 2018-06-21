using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyPressStability
{
    class KPSMain
    {
        private ConsoleKeyInfo stopKey;

        private DateTime startTime;

        private struct Data
        {
            public Data(DateTime pressTime, long interval, long offset)
            {
                this.pressTime = pressTime;
                this.interval = interval;
                this.offset = offset;
            }

            public DateTime pressTime;
            public long interval;
            public long offset;

        }

        private long totalTime;
        private long totalOffset;
        private int totalPress;
        private Data last;

        public KPSMain(out bool rtn)
        {
            rtn = Initialize();
        }

        private bool Initialize()
        {
            Console.Clear();
            Console.WriteLine("If you want any help, press H.");

            startTime = DateTime.UtcNow;
            while ((DateTime.UtcNow - startTime).TotalMilliseconds < 2000)
            {
                while (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.H)
                    {
                        ShowHelp(true);
                        return false;
                    }
                }
            }

            Console.WriteLine("Set a key to stop this program: ");
            stopKey = Console.ReadKey();

            return true;
        }

        public void Start()
        {
            Console.Clear();
            var key = Console.ReadKey(true);

            if (key == stopKey)
                return;

            startTime = DateTime.UtcNow;
            last = new Data(startTime, 0, 0);

            PrintResult(last);

            while (true)
            {
                while (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    if (key == stopKey)
                        return;
                    var cur = DateTime.UtcNow;
                    var interv = (long) (cur - last.pressTime).TotalMilliseconds;
                    var offset = interv - last.interval;
                    last = new Data(cur, interv, offset);
                    totalTime += interv;
                    totalOffset += offset;
                    totalPress++;

                    PrintResult(last);
                }
            }
        }

        private void PrintResult(Data data)
        {
            var avgapm = 60000f * totalPress / totalTime;
            var apm = 60000f / data.interval;
            Console.WriteLine("{0:hh:mm:ss.fff} - interval: {1:D}; offset: {2:D}, average: {3:F2}; apm: {4:F0}, average: {5:F0}",
                data.pressTime, data.interval,
                data.offset, (float)totalOffset / totalPress,
                apm, avgapm);
        }

        public void ShowHelp(bool clear)
        {
            if (clear)
                Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This program tests your stability of pressing keys and/or APM.\nPress any key to quit this program now.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
