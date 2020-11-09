using Microsoft.Xna.Framework;
using System;

public struct Distance
{

	public static float Space1D (float a, float b)
	{
		
		return (float)Math.Sqrt ( (a- b) *  (a - b) );

	}

	public static float Space2D (Vector2 a, Vector2 b)
	{
		return (float)Math.Sqrt ( (a.X - b.X) *  (a.X - b.X)  +  (a.Y - b.Y) *  (a.Y - b.Y) );

	}

	public static float Space3D (Vector3 a, Vector3 b)
	{
		return (float)Math.Sqrt ( (a.X - b.X) *  (a.X - b.X )  +  (a.Y - b.Y) *  (a.Y - b.Y) +  (a.Z - b.Z) *  (a.Z - b.Z) );

	}

}
