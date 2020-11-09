
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

        

            public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];
            int offset = 0;
            for (int x = 0; x < list.Length; x++)
            {
                list[x].CopyTo(result, offset);
                offset += list[x].Length;
            }
            return result;
        }
    }
}
