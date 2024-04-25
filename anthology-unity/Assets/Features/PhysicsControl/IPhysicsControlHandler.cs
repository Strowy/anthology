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
	
	void Step(float deltaTime);
}