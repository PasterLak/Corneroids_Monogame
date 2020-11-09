//using System;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Corneroids
//{
//	public class SpaceDustManager : IDisposable
//	{
//		private readonly Vector3 cCoveredArea;

//		private readonly Vector2 cDustSize;

//		private readonly Vector4 cDustUV;

//		private BillboardBatch billboardBatch;

//		private Position3[] dustPositions;

//		private Texture2D dustTexture;

//		public SpaceDustManager()
//		{
//			this.cCoveredArea = Vector3.One * 256f;
//			this.cDustSize = Vector2.One * 0.5f;
//			this.cDustUV = new Vector4(0f, 0f, 1f, 1f);
//			this.dustTexture = Engine.contentManager.Load<Texture2D>("Textures/Sprites/dust");
//		}

//		public void Dispose()
//		{
//			if (this.billboardBatch != null)
//			{
//				this.billboardBatch.Dispose();
//			}
//		}

//		public void InitializeDust(int particleCount)
//		{
//			particleCount = Math.Min(512, Math.Max(0, particleCount));
//			if (this.billboardBatch == null)
//			{
//				this.billboardBatch = new BillboardBatch(particleCount);
//			}
//			else if (this.billboardBatch.MaxBillboardsPerBatch < particleCount)
//			{
//				this.billboardBatch.Dispose();
//				this.billboardBatch = new BillboardBatch(particleCount);
//			}
//			if (particleCount > 0)
//			{
//				this.dustPositions = new Position3[particleCount];
//				Random random = new Random();
//				for (int i = 0; i < particleCount; i++)
//				{
//					this.dustPositions[i] = new Position3(new Vector3((float)random.NextDouble() * this.cCoveredArea.X, (float)random.NextDouble() * this.cCoveredArea.Y, (float)random.NextDouble() * this.cCoveredArea.Z));
//				}
//			}
//		}

//		public void Render(Camera camera)
//		{
//			if (this.dustPositions != null && this.dustTexture != null && camera != null && this.billboardBatch != null)
//			{
//				Color color;
//				color = new Color(0.4f, 0.4f, 0.4f, 1f);
//				this.billboardBatch.Begin();
//				for (int i = 0; i < this.dustPositions.Length; i++)
//				{
//					if ((this.dustPositions[i] - camera.position).LengthSquared() > 256f)
//					{
//						this.billboardBatch.DrawBillboard(this.dustTexture, camera.GetPositionRelativeToCamera(this.dustPositions[i]), this.cDustUV, this.cDustSize, color);
//					}
//				}
//				this.billboardBatch.End(camera);
//			}
//		}

//		public void Update(Position3 position)
//		{
//			if (this.dustPositions != null && Engine.FrameCounter.FrameNumber % 15 == 0)
//			{
//				Position3 p = new Position3(this.cCoveredArea * 0.5f);
//				Position3 position2 = new Position3(position - p);
//				Position3 position3 = position + p;
//				for (int i = 0; i < this.dustPositions.Length; i++)
//				{
//					if (this.dustPositions[i].X < position2.X)
//					{
//						this.dustPositions[i].X = position3.X - (position2.X - this.dustPositions[i].X) % (2L * p.X);
//					}
//					else if (this.dustPositions[i].X > position3.X)
//					{
//						this.dustPositions[i].X = position2.X + (this.dustPositions[i].X - position3.X) % (2L * p.X);
//					}
//					if (this.dustPositions[i].Y < position2.Y)
//					{
//						this.dustPositions[i].Y = position3.Y - (position2.Y - this.dustPositions[i].Y) % (2L * p.Y);
//					}
//					else if (this.dustPositions[i].Y > position3.Y)
//					{
//						this.dustPositions[i].Y = position2.Y + (this.dustPositions[i].Y - position3.Y) % (2L * p.Y);
//					}
//					if (this.dustPositions[i].Z < position2.Z)
//					{
//						this.dustPositions[i].Z = position3.Z - (position2.Z - this.dustPositions[i].Z) % (2L * p.Z);
//					}
//					else if (this.dustPositions[i].Z > position3.Z)
//					{
//						this.dustPositions[i].Z = position2.Z + (this.dustPositions[i].Z - position3.Z) % (2L * p.Z);
//					}
//				}
//			}
//		}
//	}
//}
