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
			clicked = false;
			this.buttonText = buttonText;
			parsedButtonText = buttonText;
			Name = buttonText;
			pressedLayer = new FieldLayer(position);
			layers.Add(pressedLayer);
			UpdateButtonText();
		}

		public override void Render()
		{
			if (!clicked && !forceChecked)
			{
				base.Render();
			}
			else
			{
				
				pressedLayer.Render();
				
			}
			if (!string.IsNullOrEmpty(buttonText))
			{
				SpriteBatch spriteBatch = Engine.spriteBatch;
				//spriteBatch.Begin();
				Vector2 vector = Engine.font.MeasureString(parsedButtonText);
				Rectangle positionAndSize = base.PositionAndSize;
				Vector2 vector2;
				vector2 = new Vector2(positionAndSize.X + positionAndSize.Width / 2f - vector.X / 2f, (positionAndSize.Y + positionAndSize.Height / 2) - vector.Y / 2f);
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
				return buttonText;
			}
			set
			{
				buttonText = value;
				UpdateButtonText();
			}
		}

		public bool ForceChecked
		{
			set
			{
				forceChecked = value;
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
				UpdateButtonText();
				pressedLayer.PositionAndSize = value;
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
				UpdateButtonText();
			}
		}

		private void UpdateButtonText()
		{
			if (!string.IsNullOrEmpty(buttonText))
			{
				buttonText = buttonText.Trim();
				parsedButtonText = buttonText;
				Vector2 size = Size;
				Vector2 vector = Engine.font.MeasureString(parsedButtonText);

				while (vector.X >= size.X && size.X >= 10f && parsedButtonText.Length > 0)
				{
					parsedButtonText = parsedButtonText.Substring(0, parsedButtonText.Length - 1);
					vector = Engine.font.MeasureString(parsedButtonText);
				}
			}
		}
	}
}
