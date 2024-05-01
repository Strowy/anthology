using System;
using Unity.Mathematics;

public static class ShapeGeometry
{
	public static float2[] GetRegularVertices(int vertexCount, float radius)
	{
		if (vertexCount < 3) throw new ArgumentException("A shape requires at least 3 vertices.");

		var vertices = new float2[vertexCount];
		var slice = 2 * math.PI / vertexCount;
		for (var i = 0; i < vertexCount; i++)
		{
			var angle = slice * i;
			var x = radius * math.cos(angle);
			var y = radius * math.sin(angle);
			vertices[i] = new float2(x, y);
		}

		return vertices;
	}

	public static bool IsWithinShape(float2 point, float2[] shape)
	{
		var windingNumber = 0;
		for (var i = 0; i < shape.Length; i++)
		{
			windingNumber += PointGeometry.WindingNumber(point, shape[i], shape[(i + 1) % shape.Length]);
		}

		return (windingNumber != 0);
	}
}