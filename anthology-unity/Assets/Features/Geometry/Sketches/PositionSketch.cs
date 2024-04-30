using System.Collections.Generic;
using UnityEngine;

public class PositionSketch : MonoBehaviour
{
	[SerializeField] private LineView _lineView;
	[SerializeField] private DiscView _discView;
	[SerializeField] private List<InputHandle> _handles;

	private readonly SketchHandleCollection _handleCollection = new();
	private MouseControlHandler _mouseControlHandler;

	private SpriteRenderer _discRenderer;
	private int _lastValue = 0;

	public void Start()
	{
		_discRenderer = _discView.GetComponent<SpriteRenderer>();
		_mouseControlHandler = gameObject.AddComponent<MouseControlHandler>();
		_mouseControlHandler.Initialise(_handleCollection, Camera.main);
		foreach (var handle in _handles)
		{
			_handleCollection.Add(handle);
		}
	}

	public void Update()
	{
		var discModel = _discView.Model;
		var edgeModel = _lineView.Model.Edge;
		var leftness = PointGeometry.Leftness(discModel.Position, edgeModel.Origin, edgeModel.End);
		if (leftness == _lastValue) return;

		_discRenderer.color = GetColourFromLeftness(leftness);
		_lastValue = leftness;
	}

	private static Color GetColourFromLeftness(int leftness)
	{
		return leftness switch
		{
			-1 => Color.red,
			0 => Color.yellow,
			1 => Color.green,
			_ => Color.white
		};
	}
}