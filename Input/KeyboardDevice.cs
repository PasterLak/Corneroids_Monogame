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
			return this.keyboardState.GetPressedKeys();
		}

		public bool GetKey(Keys key)
		{
			return this.keyboardState.IsKeyDown(key);
		}

		public bool GetKeyDown(Keys key)
		{
			return this.previousKeyboardState.IsKeyUp(key) && this.keyboardState.IsKeyDown(key);
		}

		public bool GetKeyUp(Keys key)
		{
			return this.previousKeyboardState.IsKeyDown(key) && this.keyboardState.IsKeyUp(key);
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
