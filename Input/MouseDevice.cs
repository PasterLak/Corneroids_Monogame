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

			Mouse.SetPosition(this.middleX, this.middleY);
			this.storedBehavior = MouseDevice.Behavior.Free;
			this.behavior = MouseDevice.Behavior.Free;
		}

		public override void UpdateDevice()
		{
			this.previousMouseState = this.mouseState;
			this.mouseState = Mouse.GetState();
			if (this.behavior == MouseDevice.Behavior.Wrapped)
			{
				Mouse.SetPosition(this.middleX, this.middleY);
			}
		}

		//public bool LeftClick()
		//{
		//	if(mouseState.LeftButton == null && this.previousMouseState.LeftButton)
		//	{ }
		//	return this.mouseState.LeftButton = null && this.previousMouseState.LeftButton = 1;
		//}

		//public bool LeftDown()
		//{
		//	return this.mouseState.LeftButton == 1;
		//}

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
			this.behavior = this.storedBehavior;
		}

		public int Scroll()
		{
			return this.mouseState.ScrollWheelValue - this.previousMouseState.ScrollWheelValue;
		}

		public void StoreState()
		{
			this.storedBehavior = this.behavior;
		}

		public int GetTranslationX()
		{
			if (this.behavior == MouseDevice.Behavior.Wrapped)
			{
				return this.mouseState.X - this.middleX;
			}
			return this.mouseState.X - this.previousMouseState.X;
		}

		public int GetTranslationY()
		{
			if (this.behavior == MouseDevice.Behavior.Wrapped)
			{
				return this.mouseState.Y - this.middleY;
			}
			return this.mouseState.Y - this.previousMouseState.Y;
		}

		public MouseDevice.Behavior CursorBehavior
		{
			get
			{
				return this.behavior;
			}
			set
			{
				this.behavior = value;
				if (value == MouseDevice.Behavior.Wrapped)
				{
					this.UpdateDevice();
				}
			}
		}

		public Point Position
		{
			get
			{
				return new Point(this.mouseState.X, this.mouseState.Y);
			}
		}

		public enum Behavior
		{
			Wrapped,
			Free
		}
	}
}
