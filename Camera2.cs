using System;
using Microsoft.Xna.Framework;

namespace Corneroids
{
	public class Camera2 
	{
		private BoundingFrustum boundingFrustum;

		private Camera2.Mode mode;

		private Matrix projectionMatrix;

		private Matrix viewMatrix;

		private Matrix viewMatrixModification;

		private Quaternion orientation;

		private Vector3 position;

		private float yaw;

		private float pitch;

		private Quaternion coordinateSpace;

		public Camera2(float aspectRatio, float fov, float nearPlane, float farPlane)
		{
			this.UpdateViewMatrix();
			this.SetAttributes(aspectRatio, fov, nearPlane, farPlane);
			this.coordinateSpace = Quaternion.Identity;
			this.mode = Camera2.Mode.Space;
			this.viewMatrixModification = Matrix.Identity;
			this.boundingFrustum = new BoundingFrustum(this.viewMatrix * this.projectionMatrix);
		}

		public Camera2()
		{
			//SettingsManager settingsManager = Engine.SettingsManager;
			this.SetAttributes(Engine.graphicsDevice.DisplayMode.AspectRatio, MathHelper.ToRadians(45f), 0.1f, 800f);
			this.UpdateViewMatrix();
			this.coordinateSpace = Quaternion.Identity;
			this.mode = Camera2.Mode.Space;
			this.viewMatrixModification = Matrix.Identity;
			this.boundingFrustum = new BoundingFrustum(this.viewMatrix * this.projectionMatrix);
			//Engine.SettingsManager.CameraAttributesChanged += this.CameraAttributesChanged;
		}

		public void Dispose()
		{
			//Engine.SettingsManager.CameraAttributesChanged -= this.CameraAttributesChanged;
		}

		public Vector3 GetLookatVector()
		{
			return Vector3.Transform(-Vector3.UnitZ, this.WorldOrientation);
		}

		public Vector2? GetPositionInScreenSpace(Vector3 worldPosition)
		{
			Vector3 positionRelativeToCamera = this.GetPositionRelativeToCamera(worldPosition);
			Vector3 vector = Engine.graphicsDevice.Viewport.Project(positionRelativeToCamera, this.projectionMatrix, this.viewMatrix, Matrix.Identity);
			if (vector.Z < 0f || vector.Z > 1f)
			{
				return null;
			}
			return new Vector2?(new Vector2(vector.X, vector.Y));
		}

		public Vector3 GetPositionRelativeToCamera(Vector3 position)
		{
			return position - this.position;
		}

		public Vector3 GetStrafeVector()
		{
			return Vector3.Transform(Vector3.UnitX, Quaternion.Concatenate(orientation, this.coordinateSpace));
		}

		public Vector3 GetUpVector()
		{
			return Vector3.Cross(this.GetStrafeVector(), this.GetLookatVector());
		}

		public void LookAt(Vector3 direction)
		{
			if (direction == Vector3.Zero)
			{
				return;
			}
			direction = Vector3.Normalize(direction);
			direction = Vector3.Transform(direction, Quaternion.Conjugate(this.CoordinateSpace));
			this.yaw = (float)Math.Atan2((double)(-(double)direction.X), (double)(-(double)direction.Z));
			this.pitch = (float)Math.Asin((double)direction.Y);
			orientation = Quaternion.CreateFromYawPitchRoll(this.yaw, this.pitch, 0f);
			this.UpdateMatrices();
		}

		public void LookAtPoint(Vector3 point)
		{
			this.LookAt(point - this.position);
		}

		public void Move(float amount)
		{
			this.Position += this.GetLookatVector() * amount;
			this.UpdateMatrices();
		}

		public void ResetDirection()
		{
			this.yaw = (this.pitch = 0f);
			this.UpdateQuaternions(this.yaw, this.pitch, 0f);
			this.UpdateMatrices();
		}

		public void Roll(float amount)
		{
			this.UpdateQuaternions(0f, 0f, amount);
		}

		public void Rotate(float yaw, float pitch)
		{
			this.pitch += pitch;
			this.yaw += yaw;
			this.pitch = Math.Max(Math.Min(this.pitch, 1.57079637f), -1.57079637f);
			this.yaw = MathHelper.WrapAngle(this.yaw);
			this.UpdateQuaternions(yaw, pitch, 0f);
			this.UpdateMatrices();
		}

		public void SetAttributes(float aspectRatio, float fov, float nearPlane, float farPlane)
		{
			this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearPlane, farPlane);
		}

		public virtual void Update(float deltaTime)
		{
			this.coordinateSpace.Normalize();
			this.UpdateCamera();
		}

		protected void UpdateCamera()
		{
			this.UpdateMatrices();
		}

		public BoundingFrustum ViewFrustum
		{
			get
			{
				return this.boundingFrustum;
			}
		}

		public Matrix ProjectionMatrix
		{
			get
			{
				return this.projectionMatrix;
			}
			set
			{
				this.projectionMatrix = value;
			}
		}

		public Matrix ViewMatrix
		{
			get
			{
				return this.viewMatrix;
			}
		}

		public Matrix ViewMatrixModification
		{
			set
			{
				this.viewMatrixModification = value;
			}
		}

		public Vector3 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
				this.UpdateViewMatrix();
				this.UpdateBoundingFrustum(this.boundingFrustum, ref this.viewMatrix, ref this.projectionMatrix);
			}
		}

		public Quaternion CoordinateSpace
		{
			get
			{
				return this.coordinateSpace;
			}
			set
			{
				this.coordinateSpace = value;
			}
		}

		public Camera2.Mode CameraMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		public Quaternion WorldOrientation
		{
			get
			{
				return Quaternion.Concatenate(orientation, this.coordinateSpace);
			}
		}

		private void UpdateBoundingFrustum(BoundingFrustum frustum, ref Matrix view, ref Matrix projection)
		{
			frustum.Matrix = view * projection;
		}

		public void UpdateMatrices()
		{
			this.UpdateViewMatrix();
			this.UpdateBoundingFrustum(this.boundingFrustum, ref this.viewMatrix, ref this.projectionMatrix);
		}

		private void UpdateQuaternions(float deltaYaw, float deltaPitch, float deltaRoll)
		{
			switch (this.mode)
			{
			case Camera2.Mode.Fixed_plane:
				orientation = Quaternion.CreateFromYawPitchRoll(this.yaw, this.pitch, 0f);
				return;
			case Camera2.Mode.Space:
				orientation *= Quaternion.CreateFromYawPitchRoll(deltaYaw, deltaPitch, deltaRoll);
				return;
			default:
				return;
			}
		}

		private void UpdateViewMatrix()
		{
			Matrix matrix = Matrix.CreateFromQuaternion(Quaternion.Conjugate(this.coordinateSpace));
			Matrix matrix2 = Matrix.CreateFromQuaternion(Quaternion.Conjugate(orientation));
			this.viewMatrix = matrix * matrix2 * this.viewMatrixModification;
		}

		private void CameraAttributesChanged()
		{
			//SettingsManager settingsManager = Engine.SettingsManager;
			//this.SetAttributes(settingsManager.AspectRatio, settingsManager.CameraFoV, 0.1f, 800f);
			//this.boundingFrustum = new BoundingFrustum(this.viewMatrix * this.projectionMatrix);
		}

		public enum Mode
		{
			Fixed_plane,
			Space
		}
	}
}
