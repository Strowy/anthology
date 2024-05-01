using UnityEngine;

public class ShapeSketch : MonoBehaviour
{
	[SerializeField] private ShapeView _shapeView;
	[SerializeField] private DiscView _discView;

	private readonly SketchHandleCollection _handleCollection = new();
	private MouseControlHandler _mouseControlHandler;

	private SpriteRenderer _discRenderer;
	private bool _lastValue;

	public void Start()
	{
		_shapeView.OnGenerate += HandleGenerate;
		
		_discRenderer = _discView.GetComponent<SpriteRenderer>();
		_mouseControlHandler = gameObject.AddComponent<MouseControlHandler>();
		_mouseControlHandler.Initialise(_handleCollection, Camera.main);

		var discModel = _discView.Model;
		var shapeModel = _shapeView.GetVertices();
		var result = ShapeGeometry.IsWithinShape(discModel.Position, shapeModel);
		_discRenderer.color = result ? Color.green : Color.red;
		_lastValue = result;
	}

	public void Update()
	{
		var discModel = _discView.Model;
		var shapeModel = _shapeView.GetVertices();
		var result = ShapeGeometry.IsWithinShape(discModel.Position, shapeModel);
		if (result == _lastValue) return;

		_discRenderer.color = result ? Color.green : Color.red;
		_lastValue = result;
	}

	private void HandleGenerate()
	{
		_handleCollection.Clear();
		_handleCollection.Add(_discView.Handle);
		var shapeHandles = _shapeView.Handles;
		foreach (var handle in shapeHandles)
		{
			_handleCollection.Add(handle);
		}
	}
}