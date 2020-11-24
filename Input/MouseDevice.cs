using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Corneroids
{
	public class MouseDevice : InputDevice
	{
		private MouseDevice.Behavior storedBehavior;

		private MouseDevice.Behavior behavior;

		private MouseState mouseState;

		private MouseState previousMouseState;

		private int middleX = 512; //512

		private int middleY = 384; //384

		public MouseDevice()
		{
			//middleX = Engine.graphicsDevice.Viewport.Width/ 2;
			//middleY = Engine.graphicsDevice.Viewport.Height / 2;

			Mouse.SetPosition(middleX, middleY);
			storedBehavior = MouseDevice.Behavior.Free;
			behavior = MouseDevice.Behavior.Free;
		}

		public override void UpdateDevice()
		{
			previousMouseState = mouseState;
			mouseState = Mouse.GetState();
			if (behavior == MouseDevice.Behavior.Wrapped)
			{
				Mouse.SetPosition(middleX, middleY);
			}
		}

        public bool LeftClick()
        {
			return mouseState.LeftButton == ButtonState.Released
				&& previousMouseState.LeftButton == ButtonState.Pressed;

		}

        public bool LeftDown()
        {
			if (mouseState.LeftButton == ButtonState.Pressed)
				return true;
			else
				return false;
			
        }

        //public bool RightClick()
        //{
        //	return this.mouseState.RightButton == null && this.previousMouseState.RightButton == 1;
        //}

        //public bool RightDown()
        //{

        //	return mouseState.RightButton;
        //}

        //public bool MiddleClick()
        //{
        //	return this.mouseState.MiddleButton == null && this.previousMouseState.MiddleButton == 1;
        //}

        //public bool MiddleDown()
        //{
        //	return this.mouseState.MiddleButton == 1;
        //}

        public void RestoreState()
		{
			behavior = storedBehavior;
		}

		public int Scroll()
		{
			return mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
		}

		public void StoreState()
		{
			storedBehavior = behavior;
		}

		public int GetTranslationX()
		{
			if (behavior == MouseDevice.Behavior.Wrapped)
			{
				return mouseState.X - middleX;
			}
			return mouseState.X - previousMouseState.X;
		}

		public int GetTranslationY()
		{
			if (behavior == MouseDevice.Behavior.Wrapped)
			{
				return mouseState.Y - middleY;
			}
			return mouseState.Y - previousMouseState.Y;
		}

		public MouseDevice.Behavior CursorBehavior
		{
			get
			{
				return behavior;
			}
			set
			{
				behavior = value;
				if (value == MouseDevice.Behavior.Wrapped)
				{
					UpdateDevice();
				}
			}
		}

		public Point Position
		{
			get
			{
				return new Point(mouseState.X, mouseState.Y);
			}
		}

		public enum Behavior
		{
			Wrapped,
			Free
		}
	}
}
