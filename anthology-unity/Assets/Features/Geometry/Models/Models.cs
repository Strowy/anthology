using Unity.Mathematics;

public struct DiscModel
{
	public float2 Position;
	public float Radius;
}

public struct EdgeModel
{
	public float2 Origin;
	public float2 End;
}

public struct CapsuleModel
{
	public EdgeModel Edge;
	public float Radius;
}