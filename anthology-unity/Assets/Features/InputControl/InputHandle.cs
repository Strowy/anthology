using UnityEngine;

public class InputHandle : MonoBehaviour, IInputHandle
{
	[SerializeField] private float _holdRadius = 0.5f;
	private float _radius;

	public Vector3 WorldPosition => transform.position;

	public float Radius { get; set; }

	public void Awake()
	{
		Radius = _holdRadius;
	}

	public void MoveTo(Vector3 position)
	{
		transform.position = position;
	}
}