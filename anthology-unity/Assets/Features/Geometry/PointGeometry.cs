using Unity.Mathematics;

public static class PointGeometry
{
	private const float INTERSECT_EPSILON = 0.00001f;

	/// <summary>
	/// Determines if given point is left, intersecting, or right of given directional edge.
	/// </summary>
	/// <param name="point">Point to test leftness.</param>
	/// <param name="edgeStart">Origin point of edge.</param>
	/// <param name="edgeEnd">End point of edge.</param>
	/// <returns>1 if left of edge, 0 if inline/intersecting edge, -1 if right of edge.</returns>
	public static int Leftness(float2 point, float2 edgeStart, float2 edgeEnd)
	{
		var leftness = (FloatingDiff(edgeEnd.x, edgeStart.x) * FloatingDiff(point.y, edgeStart.y)) -
		               (FloatingDiff(point.x, edgeStart.x) * FloatingDiff(edgeEnd.y, edgeStart.y));
		if (IsClose(leftness, 0)) return 0;
		return leftness > 0 ? 1 : -1;
	}

	/// <summary>
	/// Gives the winding number for a given point relative to a given edge. Winding number exclusion method.
	/// If sum of result for all edges of a shape != 0, point is within shape.
	/// </summary>
	/// <param name="point">Point to test.</param>
	/// <param name="edgeStart">Origin point of edge.</param>
	/// <param name="edgeEnd">End point of edge.</param>
	/// <returns>Winding number.</returns>
	public static int WindingNumber(float2 point, float2 edgeStart, float2 edgeEnd)
	{
		if (IsLessOrEqual(edgeStart.y, point.y))
		{
			if (IsLessOrEqual(edgeEnd.y, point.y)) return 0;
			if (Leftness(point, edgeStart, edgeEnd) == 1) return 1;
		}
		else
		{
			if (!IsLessOrEqual(edgeEnd.y, point.y)) return 0;
			if (Leftness(point, edgeStart, edgeEnd) == -1) return -1;
		}

		return 0;
	}

	/// <summary>
	/// The point on the given edge closest to the given point.
	/// If not at edge ends, point-point is orthogonal to start-end.
	/// </summary>
	/// <param name="point">Point to find for.</param>
	/// <param name="edgeStart">Origin point of edge.</param>
	/// <param name="edgeEnd">End point of edge.</param>
	/// <returns></returns>
	public static float2 ClosestOnEdge(float2 point, float2 edgeStart, float2 edgeEnd)
	{
		var startDiff = FloatingDiff(point, edgeStart);
		var edgeProjection = FloatingDiff(edgeEnd, edgeStart);
		var sqrMag = SqrMagnitude(edgeProjection);
		var param = -1f;
		if (!IsClose(sqrMag, 0))
			param = math.dot(startDiff, edgeProjection) / sqrMag;
		var minPoint = param switch
		{
			< 0 => edgeStart,
			> 1 => edgeEnd,
			_ => edgeStart + (param * edgeProjection)
		};
		return minPoint;
	}

	/// <summary>
	/// The minimum distance from the given point to the given edge.
	/// If not at edge ends, the orthogonal distance.
	/// </summary>
	/// <param name="point">Point to find distance for.</param>
	/// <param name="edgeStart">Origin point of edge.</param>
	/// <param name="edgeEnd">End point of edge.</param>
	/// <returns></returns>
	public static float MinDistanceToEdge(float2 point, float2 edgeStart, float2 edgeEnd)
	{
		var minPoint = ClosestOnEdge(point, edgeStart, edgeEnd);
		var diff = FloatingDiff(point, minPoint);
		return FloatingDiff(math.sqrt(SqrMagnitude(diff)), 0);
	}

	private static bool IsLessOrEqual(float value, float target)
	{
		if (IsClose(value, target)) return true;
		return value < target;
	}

	private static bool IsLess(float value, float target)
	{
		if (IsClose(value, target)) return false;
		return value < target;
	}

	private static bool IsClose(float value, float target)
	{
		return FloatingDiff(value, target) == 0;
	}

	private static float2 FloatingDiff(float2 a, float2 b)
		=> new(FloatingDiff(a.x, b.x), FloatingDiff(a.y, b.y));

	private static float FloatingDiff(float a, float b)
		=> math.abs(a - b) < INTERSECT_EPSILON ? 0 : a - b;

	private static float SqrMagnitude(float2 vector)
		=> vector.x * vector.x + vector.y * vector.y;

	private static float Magnitude(float2 vector)
		=> math.sqrt(SqrMagnitude(vector));
}