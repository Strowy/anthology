using UnityEngine;

public class InputHandle : MonoBehaviour, IInputHandle
{
	[SerializeField] private float _holdRadius = 0.5f;
	
	public Vector3 WorldPosition => transform.position;
	public float Radius => _holdRadius;

	public void MoveTo(Vector3 position)
	{
		transform.position = position;
	}
}