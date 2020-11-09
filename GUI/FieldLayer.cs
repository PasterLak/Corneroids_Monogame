using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids.GUI
{
	public class FieldLayer : WindowLayer
	{
		public FieldLayer(Rectangle position) : base(position)
		{
			Color darkBorderColor = this.darkBorderColor;
			Color lightBorderColor = this.lightBorderColor;
			Color backgroundColor = this.backgroundColor;
			this.darkBorderColor = lightBorderColor;
			this.lightBorderColor = darkBorderColor;
			this.backgroundColor = new Color(170, 170, 170);
		}
	}
}
