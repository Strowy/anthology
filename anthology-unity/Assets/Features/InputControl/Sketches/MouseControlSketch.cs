using System.Collections.Generic;
using UnityEngine;

public class MouseControlSketch : MonoBehaviour
{
	[SerializeField] private List<InputHandle> _handles;
	
	private readonly SketchHandleCollection _handleCollection = new();
	private MouseControlHandler _mouseControlHandler;

	public void Start()
	{
		_mouseControlHandler = gameObject.AddComponent<MouseControlHandler>();
		_mouseControlHandler.Initialise(_handleCollection, Camera.main);
		foreach (var handle in _handles)
		{
			_handleCollection.Add(handle);
		}
	}

	private class SketchHandleCollection : IInputHandleCollection
	{
		public IEnumerable<IInputHandle> AvailableHandles => _handles;

		private readonly List<IInputHandle> _handles = new();

		public void Add(IInputHandle handle)
		{
			_handles.Add(handle);
		}

		public void Remove(IInputHandle handle)
		{
			_handles.Remove(handle);
		}
	}
}