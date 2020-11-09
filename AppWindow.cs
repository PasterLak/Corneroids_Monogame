using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids
{
    public class AppWindow
    {
        private const int defaultWindowWidth = 1280;
        private const int defaultWindowWHeight = 800;

        public AppWindow()
        {
           
        }

        public static void SetFullScreen()
        {
            GraphicsDeviceManager graphics = Engine.graphics;

            if(! graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferWidth = GetScreenWidth;
                graphics.PreferredBackBufferHeight = GetScreenHeight;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
            else
            {

                graphics.PreferredBackBufferWidth = defaultWindowWidth;
                graphics.PreferredBackBufferHeight = defaultWindowWHeight;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
            
              
            

        }
       

        public static void SetDefaultWindow()
        {
            GraphicsDeviceManager _graphics = Engine.graphics;

            _graphics.PreferredBackBufferWidth = defaultWindowWidth;
            _graphics.PreferredBackBufferHeight = defaultWindowWHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        public static int GetScreenWidth
        {
            
            get { return Engine.GetW(); }
        }

        public static int GetScreenHeight
        {
            get { return Engine.GetH(); }
        }

        public static Vector2 GetScreenCenter
        {
            get { return new Vector2(Engine.window.ClientBounds.Width / 2, Engine.window.ClientBounds.Height / 2); }
        }

        
       

    }
}
