﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids.GUI
{
	public class WindowLayer : Layer
	{
		protected Color darkBorderColor = new Color(127, 127, 127);

		protected Color backgroundColor = new Color(190, 190, 190);

		protected Color lightBorderColor = new Color(228, 228, 228);

		protected Texture2D blankTexture;

		public WindowLayer(Rectangle position) : base(position)
		{
			this.blankTexture = Resources.LoadTexture2D("Content/blank.png");
		}

		public override void Render()
		{
			Rectangle positionAndSize = base.PositionAndSize;
			Rectangle rectangle;
			rectangle = new Rectangle(positionAndSize.X - 5, positionAndSize.Y - 5, positionAndSize.Width + 10, positionAndSize.Height + 10);
			Rectangle rectangle2;
			rectangle2 = new Rectangle(positionAndSize.X - 5, positionAndSize.Y - 5, positionAndSize.Width + 5, positionAndSize.Height + 5);
			Rectangle rectangle3;
			rectangle3 = new Rectangle(positionAndSize.X, positionAndSize.Y, positionAndSize.Width, positionAndSize.Height);
			SpriteBatch spriteBatch = Engine.spriteBatch;
			//spriteBatch.Begin();
			spriteBatch.Draw(this.blankTexture, rectangle, this.darkBorderColor);
			spriteBatch.Draw(this.blankTexture, rectangle2, this.lightBorderColor);
			spriteBatch.Draw(this.blankTexture, rectangle3, this.backgroundColor);
			//spriteBatch.End();
		}
	}
}