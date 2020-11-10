using System;
using System.IO;


namespace Corneroids
{
    public class Debug
    {

        public static void Write(object obj)
        {
            System.Console.Write(obj);
        }

        public static void WriteLine(object obj)
        {

            System.Console.WriteLine(obj);
            
        }

        public static void WriteLine(object obj, ConsoleColor color)
        {

            ConsoleColor standartColor = System.Console.BackgroundColor;

            System.Console.BackgroundColor = color;

            System.Console.WriteLine(obj);

            System.Console.BackgroundColor = standartColor;

        }

        public static void Error(object obj)
        {

            ConsoleColor color = System.Console.BackgroundColor;

            System.Console.BackgroundColor = ConsoleColor.Red;

            System.Console.WriteLine(obj);

            System.Console.BackgroundColor = color;
        }

        public static void Clear()
        {
            System.Console.Clear();

            
        }

    }
}
