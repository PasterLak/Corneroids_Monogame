using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Corneroids.GUI
{
	public abstract class Layer
	{
		protected List<Layer> layers;

		private Rectangle positionAndSize;

		private string name;

		private object tag;

		public Layer(Rectangle positionAndSize)
		{
			this.layers = new List<Layer>();
			this.positionAndSize = positionAndSize;
			this.positionAndSize.Width = Math.Max(this.positionAndSize.Width, 1);
			this.positionAndSize.Height = Math.Max(this.positionAndSize.Height, 1);
		}

		public virtual bool Contains(Point location)
		{
			return this.positionAndSize.Contains(location);
		}

		public static Rectangle EvaluateMiddlePosition(int width, int height)
		{
			Point point;
			point = new Point(Engine.graphicsDevice.PresentationParameters.BackBufferWidth, Engine.graphicsDevice.PresentationParameters.BackBufferHeight);
			return new Rectangle(point.X / 2 - width / 2, point.Y / 2 - height / 2, width, height);
		}

		public Layer GetClickedLayer(Point mousePosition, params Type[] acceptLayers)
		{
			if (this.positionAndSize.Contains(mousePosition))
			{
				foreach (Layer layer in this.layers)
				{
					Layer clickedLayer = layer.GetClickedLayer(mousePosition, acceptLayers);
					if (clickedLayer != null)
					{
						return clickedLayer;
					}
				}
				if (acceptLayers.Length == 0)
				{
					return this;
				}
				if (Array.Find<Type>(acceptLayers, (Type t) => t == base.GetType()) != null)
				{
					return this;
				}
			}
			return null;
		}

		public Layer GetLayer(string name)
		{
			foreach (Layer layer in this.layers)
			{
				if (layer.Name == name)
				{
					return layer;
				}
			}
			return null;
		}

		public abstract void Render();

		public virtual Point Location
		{
			get
			{
				return new Point(this.positionAndSize.X, this.positionAndSize.Y);
			}
			set
			{
				Point point;
				point = new Point(value.X - this.Location.X, value.Y - this.Location.Y);
				this.positionAndSize = new Rectangle(value.X, value.Y, this.positionAndSize.Width, this.positionAndSize.Height);
				foreach (Layer layer in this.layers)
				{
					layer.Location = new Point(layer.Location.X + point.X, layer.Location.Y + point.Y);
				}
			}
		}

		public virtual Rectangle PositionAndSize
		{
			get
			{
				return this.positionAndSize;
			}
			set
			{
				this.positionAndSize = value;
			}
		}

		public virtual Vector2 Position
		{
			get
			{
				return new Vector2((float)this.positionAndSize.X, (float)this.positionAndSize.Y);
			}
			set
			{
				this.positionAndSize = new Rectangle((int)value.X, (int)value.Y, this.positionAndSize.Width, this.positionAndSize.Height);
			}
		}

		public virtual Vector2 Size
		{
			get
			{
				return new Vector2((float)this.positionAndSize.Width, (float)this.positionAndSize.Height);
			}
			set
			{
				this.positionAndSize = new Rectangle(this.positionAndSize.X, this.positionAndSize.Y, (int)value.X, (int)value.Y);
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		private bool PointInLayer(Point point)
		{
			return point.X >= 0 && point.Y >= 0 && point.X < this.positionAndSize.Width && point.Y < this.positionAndSize.Height;
		}
	}
}
