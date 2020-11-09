using System;
using Microsoft.Xna.Framework;

public struct Vector3Byte
{
	public byte x;
	public byte y;
	public byte z;

	public Vector3Byte(byte x, byte y, byte z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	/*public Vector3Byte( Vector3Int v)
	{
		this.x = (byte)v.x;
		this.y = (byte)v.y;
		this.z = (byte)v.z;
	}*/
	public Vector3Byte( Vector3 v)
	{
		this.x = (byte)v.X;
		this.y = (byte)v.Y;
		this.z = (byte)v.Z;
	}
	public static Vector3Byte zero
	{
		get { return new Vector3Byte(0, 0, 0); }
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
	public static explicit operator Vector3(Vector3Byte v)
	{
		return new Vector3(v.x, v.y, v.z);
	}
	public static explicit operator Vector3Byte(Vector3 v)
	{
		return new Vector3Byte(v);
	}

	public static ushort Distance(Vector3Byte a, Vector3Byte b)
	{
		return (ushort)Math.Sqrt ( (a.x - b.x) *  (a.x - b.x )  +  (a.y - b.y) *  (a.y - b.y) +  (a.z - b.z) *  (a.z - b.z) );
	}



	public override string ToString()
	{
			return string.Format("({0},{1},{2})", x, y, z);
	}
	
}