using System;
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class Settings
    {
        public static Color backgroundColor;
        public static float Fov = 90f;

        public static readonly int viewDistance ;

        
            
        static Settings()
        {
            
            viewDistance = 100;
        }

        


        

        public enum Quality
        {
            Off,
            Low,
            Hight
        }

    }
}
