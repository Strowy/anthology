using UnityEngine;

public interface IPhysicsControlHandler
{
	void Add(IPhysicsHandle handle);
	void Remove(IPhysicsHandle handle);
	void Step(float deltaTime);
}

public interface IPhysicsHandle
{
	Vector3 Velocity { get; }
	Vector2 Bounds { get; set; }
	void Step(float deltaTime);
}