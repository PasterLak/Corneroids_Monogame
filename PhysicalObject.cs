using System;
using Microsoft.Xna.Framework;

namespace CornerSpace
{
	public abstract class PhysicalObject : IDisposable
	{
		private const float cMaxRotationSpeed = 1.5f;

		private readonly float cMaxSpeed = 20f;

		private BoundingSphereI boundingSphere;

		private float boundingRadius;

		protected long elapsedInterpolationTicks;

		private Vector3 inertiaTensor;

		private float mass;

		private Quaternion orientation;

		protected Position3 position;

		private Vector3 rotation;

		protected Vector3 speed;

		private StateFrameBuffer stateBuffer;

		protected Matrix inverseTransformMatrix;

		protected Matrix transformMatrix;

		public PhysicalObject()
		{
			this.elapsedInterpolationTicks = 0L;
			this.orientation = Quaternion.Identity;
			this.inertiaTensor = Vector3.Zero;
			this.mass = 1f;
			this.position = Position3.Zero;
			this.rotation = Vector3.Zero;
			this.speed = Vector3.Zero;
			this.inverseTransformMatrix = Matrix.Identity;
			this.transformMatrix = Matrix.Identity;
			this.stateBuffer = new StateFrameBuffer(64);
			if (Engine.FastTravel)
			{
				this.cMaxSpeed = 100f;
			}
		}

		public virtual void ApplyForce(Vector3 point, Vector3 force, float deltaTime)
		{
		}

		public virtual void Dispose()
		{
		}

		public virtual bool GetCollision(PhysicalObject physicalObject, ref CollisionData collisionData)
		{
			throw new NotImplementedException();
		}

		public virtual Vector3 GetSpeedAt(Position3 position)
		{
			return this.speed;
		}

		public virtual void InterpolatePosition()
		{
			this.elapsedInterpolationTicks += Engine.FrameCounter.DeltaTimeMS;
			StateFrame stateFrame = this.stateBuffer.PeekLatestState();
			StateFrame stateFrame2 = this.stateBuffer.PeekHistoryState(1);
			int num = (int)(stateFrame.Tick - stateFrame2.Tick);
			if (num > 0)
			{
				Vector3 vector = stateFrame.Position - stateFrame2.Position;
				float num2 = MathHelper.Clamp((float)this.elapsedInterpolationTicks / (float)num, 0f, 1f);
				this.Position = stateFrame2.Position + vector * num2;
				this.Orientation = Quaternion.Lerp(stateFrame2.Orientation, stateFrame.Orientation, num2);
			}
		}

		public virtual bool UpdateBasedOnLatestState()
		{
			if (this.stateBuffer.HasUnhandledStates())
			{
				this.stateBuffer.GetLatestState();
				StateFrame stateFrame = this.stateBuffer.PeekHistoryState(1);
				this.Position = stateFrame.Position;
				this.Orientation = stateFrame.Orientation;
				this.elapsedInterpolationTicks = 0L;
				return true;
			}
			return false;
		}

		public virtual void UpdatePhysics(float deltaTime)
		{
			float num = this.speed.Length();
			if (num > this.cMaxSpeed)
			{
				this.speed = Vector3.Normalize(this.speed) * this.cMaxSpeed;
			}
			float num2 = this.rotation.Length();
			if (num2 > 1.5f)
			{
				this.rotation = Vector3.Normalize(this.rotation) * 1.5f;
			}
			this.orientation.Normalize();
		}

		public virtual BoundingSphereI BoundingSphere
		{
			get
			{
				return this.boundingSphere;
			}
			protected set
			{
				this.boundingSphere = value;
			}
		}

		public float BoundingRadius
		{
			get
			{
				return this.boundingRadius;
			}
			set
			{
				this.boundingRadius = value;
			}
		}

		public Vector3 InertiaTensor
		{
			get
			{
				return this.inertiaTensor;
			}
			set
			{
				this.inertiaTensor = value;
			}
		}

		public Quaternion Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				this.orientation = value;
			}
		}

		public Vector3 Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		public Quaternion RotationQuaternion
		{
			get
			{
				if (this.rotation == Vector3.Zero)
				{
					return Quaternion.Identity;
				}
				return Quaternion.CreateFromAxisAngle(Vector3.Normalize(this.Rotation), this.Rotation.Length() * Engine.FrameCounter.DeltaTime);
			}
		}

		public virtual Position3 Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
				this.boundingSphere.Center = value;
			}
		}

		public virtual Vector3 Speed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.speed = value;
			}
		}

		public StateFrameBuffer StateBuffer
		{
			get
			{
				return this.stateBuffer;
			}
		}

		public Matrix InverseTransformMatrix
		{
			get
			{
				return this.inverseTransformMatrix;
			}
		}

		public Matrix TransformMatrix
		{
			get
			{
				return this.transformMatrix;
			}
		}

		public float Mass
		{
			get
			{
				return this.mass;
			}
			set
			{
				this.mass = value;
			}
		}
	}
}
