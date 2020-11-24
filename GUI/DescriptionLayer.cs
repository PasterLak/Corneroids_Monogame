using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids.GUI
{
	public class DescriptionLayer : WindowLayer
	{
		private List<string> lines;

		private float lineSize;

		public DescriptionLayer(List<string> lines) : base(Rectangle.Empty)
		{
			this.lines = new List<string>();
			Lines = lines;
		}

		public void Addline(string line)
		{
			if (!string.IsNullOrEmpty(line))
			{
				lines.Add(line);
			}
			UpdateLines();
		}
		
		public override void Render()
		{
			base.Render();
			if (lines != null)
			{
				Vector2 vector = Position;
				//Engine.spriteBatch.Begin();

				foreach (string text in lines)
				{
					if (!string.IsNullOrEmpty(text))
					{
						Engine.spriteBatch.DrawString(Engine.font, text, vector, Color.White);
						vector += Vector2.UnitY * lineSize;
					}
				}
				//Engine.spriteBatch.End();
			}
		}

		public void SetLine(string line)
		{
			lines.Clear();
			if (!string.IsNullOrEmpty(line))
			{
				lines.Add(line);
			}
			UpdateLines();
		}

		public List<string> Lines
		{
			set
			{
				lines.Clear();
				if (value != null)
				{
					foreach (string item in value)
					{
						lines.Add(item);
					}
					UpdateLines();
				}
			}
		}

		private void UpdateLines()
		{
			Point point;
			point = new Point(10, 10);
			foreach (string text in this.lines)
			{
				if (!string.IsNullOrEmpty(text))
				{
					Vector2 vector = Engine.font.MeasureString(text);
					point = new Point((int)Math.Max((float)point.X, vector.X), (int)((float)point.Y + vector.Y));
					this.lineSize = (float)((int)vector.Y);
				}
			}
			this.Size = new Vector2((float)point.X, (float)point.Y);
		}
	}
}
