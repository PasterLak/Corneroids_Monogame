using System;
using Microsoft.Xna.Framework;

namespace Corneroids
{
	public struct Position3
	{
		private const int floatToLong = 8388608;

		private const float longToFloat = 1.1920929E-07f;

		public long X;

		public long Y;

		public long Z;

		public Position3(long x, long y, long z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public Position3(Vector3 vector)
		{
			this.X = Position3.PackFloat(vector.X);
			this.Y = Position3.PackFloat(vector.Y);
			this.Z = Position3.PackFloat(vector.Z);
		}

		public static Position3 Floor(Position3 value)
		{
			return new Position3(value.X & -8388608L, value.Y & -8388608L, value.Z & -8388608L);
		}

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{ ",
				this.X.ToString(),
				", ",
				this.Y.ToString(),
				", ",
				this.Z.ToString(),
				" }"
			});
		}

		public Vector3 ToVector3()
		{
			return new Vector3(Position3.UnpackFloat(this.X), Position3.UnpackFloat(this.Y), Position3.UnpackFloat(this.Z));
		}

		public static Position3 operator +(Position3 p, Vector3 v)
		{
			return p + new Position3(v);
		}

		public static Position3 operator -(Position3 p, Vector3 v)
		{
			return new Position3(p.X - Position3.PackFloat(v.X), p.Y - Position3.PackFloat(v.Y), p.Z - Position3.PackFloat(v.Z));
		}

		public static Position3 operator +(Vector3 v, Position3 p)
		{
			return p + v;
		}

		public static Position3 operator +(Position3 p1, Position3 p2)
		{
			return new Position3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
		}

		public static Vector3 operator -(Position3 p1, Position3 p2)
		{
			return new Vector3(Position3.UnpackFloat(p1.X - p2.X), Position3.UnpackFloat(p1.Y - p2.Y), Position3.UnpackFloat(p1.Z - p2.Z));
		}

		public static Position3 Zero
		{
			get
			{
				return new Position3(0L, 0L, 0L);
			}
		}

		private static long PackFloat(float value)
		{
			return (long)(value * 8388608f);
		}

		private static float UnpackFloat(long value)
		{
			return (float)value * 1.1920929E-07f;
		}
	}
}
