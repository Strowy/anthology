using System;
using UnityEngine;

public class ThrowableHandle : MonoBehaviour, IPhysicsHandle, IInputHandle
{
	[SerializeField] private float _holdRadius = 0.5f;

	public Vector3 WorldPosition => transform.position;
	public float Radius => _holdRadius;
	public Vector3 Velocity { get; private set; } = Vector3.zero;

	private Vector3 _targetPosition = Vector3.zero;
	private bool _override;

	public void MoveTo(Vector3 position)
	{
		_override = true;
		_targetPosition = position;
	}

	public void Step(float deltaTime)
	{
		if (_override)
		{
			Velocity = (_targetPosition - WorldPosition) / deltaTime;
			_override = false;
		}

		transform.position += (Velocity * deltaTime);
	}
}