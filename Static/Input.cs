
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class Input 
    {
        private static KeyboardState currentKeyState;
        private static KeyboardState previousKeyState;

        
        public static bool GetButton(Keys key)
        {
            
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static bool GetButtonUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        public static bool GetButtonDown (Keys key)
        {
            
            return false;
        }

     
    }
}
