using System;
using Microsoft.Xna.Framework;

public struct Vector3Sbyte
{
	public sbyte x;
	public sbyte y;
	public sbyte z;

	public Vector3Sbyte(sbyte x, sbyte y, sbyte z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public Vector3Sbyte( Vector3 v)
	{
		x = (sbyte)v.X;
		y = (sbyte)v.Y;
		z = (sbyte)v.Z;
	}
	public static Vector3Sbyte Zero
	{
		get { return new Vector3Sbyte(0, 0, 0); }
	}

	public static Vector3Sbyte Forward
	{
		get { return new Vector3Sbyte(0, 0, -1); }
	}
	public static Vector3Sbyte Backward
	{
		get { return new Vector3Sbyte(0, 0, 1); }
	}
	public static Vector3Sbyte Left
	{
		get { return new Vector3Sbyte(-1, 0, 0); }
	}
	public static Vector3Sbyte Right
	{
		get { return new Vector3Sbyte(1, 0, 0); }
	}
	public static Vector3Sbyte Up
	{
		get { return new Vector3Sbyte(0, 1, 0); }
	}
	public static Vector3Sbyte Down
	{
		get { return new Vector3Sbyte(0, -1, 0); }
	}
	public static Vector3Sbyte operator +(Vector3Sbyte a, Vector3Sbyte b)
	{
		sbyte x,y,z;

		if (a.x + b.x > sbyte.MaxValue) x = sbyte.MaxValue;
		else x = (sbyte)(a.x + b.x);

		if(a.y + b.y > sbyte.MaxValue) y = sbyte.MaxValue;
		else y = (sbyte)(a.y + b.y);

		if(a.z + b.z > sbyte.MaxValue) z = sbyte.MaxValue;
		else z = (sbyte)(a.z + b.z);

		return new Vector3Sbyte(x,y,z);
	}
	/*public static explicit operator Vector3Int(Vector3Byte v)
	{
		return new Vector3Int(v.x, v.y, v.z);
	}
	public static explicit operator Vector3Byte(Vector3Int v)
	{
		return new Vector3Byte(v);
	}
	*/
	public static explicit operator Vector3(Vector3Sbyte v)
	{
		return new Vector3(v.x, v.y, v.z);
	}
	public static explicit operator Vector3Sbyte(Vector3 v)
	{
		return new Vector3Sbyte(v);
	}

	public static ushort Distance(Vector3Sbyte a, Vector3Sbyte b)
	{
		return (ushort)Math.Sqrt ( (a.x - b.x) *  (a.x - b.x )  +  (a.y - b.y) *  (a.y - b.y) +  (a.z - b.z) *  (a.z - b.z) );
	}



	public override string ToString()
	{
			return string.Format("({0},{1},{2})", x, y, z);
	}
	
}