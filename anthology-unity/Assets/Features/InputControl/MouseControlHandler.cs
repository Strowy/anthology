using UnityEngine;

public class MouseControlHandler : MonoBehaviour, IInputControlHandler
{
	public Vector3 WorldPosition { get; private set; }
	public bool Handling { get; private set; }
	public IInputHandle Held { get; private set; }

	private Camera _camera;
	private IInputHandleCollection _handleCollection;
	private bool _initialised;

	private Vector3 _holdOffset;

	public void Initialise(IInputHandleCollection handleCollection, Camera referenceCamera)
	{
		_handleCollection = handleCollection;
		_camera = referenceCamera;
		_initialised = true;
	}

	public void Update()
	{
		if (!_initialised) return;

		UpdatePosition();
		UpdateInput();
	}

	private void UpdatePosition()
	{
		var mousePosition = Input.mousePosition;
		var position = _camera.ScreenToWorldPoint(mousePosition);
		position.z = 0;
		WorldPosition = position;
	}

	private void UpdateInput()
	{
		if (!Handling && Input.GetMouseButtonDown(0))
		{
			Handling = true;
			TryHandling();
		}

		if (!Handling) return;
		
		Held?.MoveTo(WorldPosition + _holdOffset);

		if (!Input.GetMouseButtonUp(0)) return;
		
		Handling = false;
		Held = null;
	}

	private void TryHandling()
	{
		var found = false;
		var currentDist = float.MaxValue;
		IInputHandle currentHandle = null;

		foreach (var handle in _handleCollection.AvailableHandles)
		{
			var offset = handle.WorldPosition - WorldPosition;
			var sqrDist = offset.sqrMagnitude;
			if (sqrDist > currentDist || sqrDist > handle.Radius * handle.Radius) continue;

			found = true;
			currentDist = sqrDist;
			currentHandle = handle;
		}

		if (!found) return;

		Held = currentHandle;
		_holdOffset = Held.WorldPosition - WorldPosition;
	}
}