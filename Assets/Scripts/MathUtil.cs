using UnityEngine;
using System;

public static class MathUtil
{
	public static float VectorToAngle(Vector2 vector)
	{
		return (float)Math.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
	}

	public static Vector3 AngleToVector(float angle)
	{
		angle *= Mathf.Deg2Rad;
		return new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle));
	}
}
