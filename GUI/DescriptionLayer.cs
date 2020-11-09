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
			this.Lines = lines;
		}

		public void Addline(string line)
		{
			if (!string.IsNullOrEmpty(line))
			{
				this.lines.Add(line);
			}
			this.UpdateLines();
		}

		public override void Render()
		{
			base.Render();
			if (this.lines != null)
			{
				Vector2 vector = this.Position;
				//Engine.spriteBatch.Begin();

				foreach (string text in this.lines)
				{
					if (!string.IsNullOrEmpty(text))
					{
						Engine.spriteBatch.DrawString(Engine.font, text, vector, Color.White);
						vector += Vector2.UnitY * this.lineSize;
					}
				}
				//Engine.spriteBatch.End();
			}
		}

		public void SetLine(string line)
		{
			this.lines.Clear();
			if (!string.IsNullOrEmpty(line))
			{
				this.lines.Add(line);
			}
			this.UpdateLines();
		}

		public List<string> Lines
		{
			set
			{
				this.lines.Clear();
				if (value != null)
				{
					foreach (string item in value)
					{
						this.lines.Add(item);
					}
					this.UpdateLines();
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
