using System;
using UnityEngine;

public class ThrowableHandle : MonoBehaviour, IPhysicsHandle, IInputHandle
{
	[SerializeField] private float _holdRadius = 0.5f;
	[SerializeField] private float _drag = 0.05f;
	[SerializeField] private float _maxSpeed = 50;

	public Vector3 WorldPosition => transform.position;
	public float Radius => _holdRadius;
	public Vector3 Velocity { get; private set; } = Vector3.zero;
	public Vector2 Bounds { get; set; } = new(10, 10);

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

		if (Velocity.sqrMagnitude < 0.0001f) return;

		transform.position += (Velocity * deltaTime);

		var updatedVelocity = Velocity;

		if (WorldPosition.x > Bounds.x - Radius) updatedVelocity.x = -Math.Abs(updatedVelocity.x);
		if (WorldPosition.x < -Bounds.x + Radius) updatedVelocity.x = Math.Abs(updatedVelocity.x);
		if (WorldPosition.y > Bounds.y - Radius) updatedVelocity.y = -Math.Abs(updatedVelocity.y);
		if (WorldPosition.y < -Bounds.y + Radius) updatedVelocity.y = Math.Abs(updatedVelocity.y);

		var speed = Math.Min((updatedVelocity.magnitude * (1 - _drag * deltaTime)), _maxSpeed);
		if (speed < 0.01) speed = 0;
		
		updatedVelocity = speed * updatedVelocity.normalized;

		Velocity = updatedVelocity;
	}
}