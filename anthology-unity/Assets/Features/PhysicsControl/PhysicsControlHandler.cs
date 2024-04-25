using System.Collections.Generic;
using UnityEngine;

public class PhysicsControlHandler : MonoBehaviour, IPhysicsControlHandler
{
	[SerializeField] private Vector2 _extents = new(8, 4);

	private readonly List<IPhysicsHandle> _handles = new();

	public void Update()
	{
		Step(Time.deltaTime);
	}

	public void Add(IPhysicsHandle handle)
	{
		handle.Bounds = _extents;
		_handles.Add(handle);
	}

	public void Remove(IPhysicsHandle handle)
	{
		_handles.Remove(handle);
	}

	public void Step(float deltaTime)
	{
		foreach (var handle in _handles)
			handle.Step(deltaTime);
	}
}