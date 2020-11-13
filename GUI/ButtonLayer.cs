using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids.GUI
{
	public class ButtonLayer : WindowLayer
	{
		private const float characterWidth = 9f;

		private bool clicked;

		private string buttonText;

		private bool forceChecked;

		private string parsedButtonText;

		private FieldLayer pressedLayer;

		public delegate void clickDelegate();
		public event clickDelegate OnClicked;

		public ButtonLayer(Rectangle position, string buttonText) : base(position)
		{
			this.clicked = false;
			this.buttonText = buttonText;
			this.parsedButtonText = buttonText;
			base.Name = buttonText;
			this.pressedLayer = new FieldLayer(position);
			this.layers.Add(this.pressedLayer);
			this.UpdateButtonText();
		}

		public override void Render()
		{
			if (!this.clicked && !this.forceChecked)
			{
				base.Render();
			}
			else
			{
				
				this.pressedLayer.Render();
				
			}
			if (!string.IsNullOrEmpty(buttonText))
			{
				SpriteBatch spriteBatch = Engine.spriteBatch;
				//spriteBatch.Begin();
				Vector2 vector = Engine.font.MeasureString(parsedButtonText);
				Rectangle positionAndSize = base.PositionAndSize;
				Vector2 vector2;
				vector2 = new Vector2((float)positionAndSize.X + (float)positionAndSize.Width / 2f - vector.X / 2f, (float)(positionAndSize.Y + positionAndSize.Height / 2) - vector.Y / 2f);
				spriteBatch.DrawString(Engine.font, parsedButtonText, vector2, Color.White);
				//spriteBatch.End();
			}
		}

		public void UpdateInput(MouseDevice mouse)
		{
			clicked = false;
			
			if (mouse.LeftDown() && base.Contains(mouse.Position))
			{
				clicked = true;
				
			}
			if(mouse.LeftClick() && base.Contains(mouse.Position))
            {
				OnClicked?.Invoke();
			}
		}

		public string ButtonText
		{
			get
			{
				return this.buttonText;
			}
			set
			{
				this.buttonText = value;
				this.UpdateButtonText();
			}
		}

		public bool ForceChecked
		{
			set
			{
				this.forceChecked = value;
			}
		}

		public override Rectangle PositionAndSize
		{
			get
			{
				return base.PositionAndSize;
			}
			set
			{
				base.PositionAndSize = value;
				this.UpdateButtonText();
				this.pressedLayer.PositionAndSize = value;
			}
		}

		public override Vector2 Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
				this.UpdateButtonText();
			}
		}

		private void UpdateButtonText()
		{
			if (!string.IsNullOrEmpty(this.buttonText))
			{
				this.buttonText = this.buttonText.Trim();
				this.parsedButtonText = this.buttonText;
				Vector2 size = this.Size;
				Vector2 vector = Engine.font.MeasureString(this.parsedButtonText);
				while (vector.X >= size.X && size.X >= 10f && this.parsedButtonText.Length > 0)
				{
					this.parsedButtonText = this.parsedButtonText.Substring(0, this.parsedButtonText.Length - 1);
					vector = Engine.font.MeasureString(this.parsedButtonText);
				}
			}
		}
	}
}
