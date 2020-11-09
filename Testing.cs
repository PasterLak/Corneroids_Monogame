using System;
using System.Diagnostics;
using Corneroids;
using Microsoft.Xna.Framework;

public class Testing 
    {

        private static Stopwatch _stopwatch = new Stopwatch();
        private static Mode _mode;

        public enum Mode
        {
            detailed, 
            shortened
        }

        public static void Start(Mode mode = Mode.detailed)
        {
            _mode = mode;
            Console.WriteLine("Start Stopwatch");
            _stopwatch.Start();
        }

        public static void End(bool writeInGameConsole = false)
        {
            _stopwatch.Stop();

            if (_mode == Mode.detailed)
            {
                Console.WriteLine("Stopwatch result: " + _stopwatch.Elapsed);
                
            }
            else
            {
                TimeSpan ts = _stopwatch.Elapsed;
                
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds );

                Console.WriteLine("Stopwatch result: " + elapsedTime);
            //if (writeInGameConsole) GameConsole.SendMessage("Stopwatch result: " + elapsedTime, Color.Red);


            }
            
            _stopwatch.Reset();
           
        }

    }
