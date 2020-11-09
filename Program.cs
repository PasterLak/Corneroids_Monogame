using System;
using System.Collections.Generic;


namespace Corneroids
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Engine())
                game.Run();


        }
    }
}
