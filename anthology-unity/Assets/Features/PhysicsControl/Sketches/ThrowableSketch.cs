using System.Collections.Generic;
using UnityEngine;

public class ThrowableSketch : MonoBehaviour
{
	[SerializeField] private List<ThrowableHandle> _handles;
	
	private readonly SketchHandleCollection _handleCollection = new();
	private MouseControlHandler _mouseControlHandler;
	private PhysicsControlHandler _physicsControlHandler;

	public void Start()
	{
		_mouseControlHandler = gameObject.AddComponent<MouseControlHandler>();
		_mouseControlHandler.Initialise(_handleCollection, Camera.main);
		_physicsControlHandler = gameObject.AddComponent<PhysicsControlHandler>();
		
		foreach (var handle in _handles)
		{
			_handleCollection.Add(handle);
			_physicsControlHandler.Add(handle);
		}
	}
}