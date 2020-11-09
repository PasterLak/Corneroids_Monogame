using System;
using Microsoft.Xna.Framework.Input;

namespace Corneroids
{
	public interface IKeyboardInterface
	{
		Keys[] GetPressedKeys();

		bool GetKey(Keys key);

		bool GetKeyDown(Keys key);

		bool GetKeyUp(Keys key);
	}
}
