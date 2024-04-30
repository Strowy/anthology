using Unity.Mathematics;
using UnityEngine;

public static class Float2Ext
{
	public static Vector3 ToVector3(this float2 vector)
	{
		return new Vector3(vector.x, vector.y, 0);
	}

	public static float2 ToFloat2(this Vector3 vector)
	{
		return new float2(vector.x, vector.y);
	}
}