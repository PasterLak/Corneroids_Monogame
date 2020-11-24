using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Corneroids
{
	public class KeyboardDevice : InputDevice, IKeyboardInterface
	{
		private static Dictionary<KeyboardDevice.Action, Keys> keyBinds = new Dictionary<KeyboardDevice.Action, Keys>();

		private KeyboardState keyboardState;

		private KeyboardState previousKeyboardState;

		public override void UpdateDevice()
		{
			this.previousKeyboardState = this.keyboardState;
			this.keyboardState = Keyboard.GetState();
		}

		public Keys[] GetPressedKeys()
		{
			return keyboardState.GetPressedKeys();
		}

		public bool GetKey(Keys key)
		{
			return keyboardState.IsKeyDown(key);
		}

		public bool GetKeyDown(Keys key)
		{
			return previousKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
		}

		public bool GetKeyUp(Keys key)
		{
			return previousKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
		}

		public static Dictionary<KeyboardDevice.Action, Keys> KeyBinds
		{
			get
			{
				return KeyboardDevice.keyBinds;
			}
		}

		public enum Action
		{
			Edit,
			Use
		}
	}
}
