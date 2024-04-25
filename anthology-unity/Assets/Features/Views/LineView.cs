using UnityEngine;

public class LineView : MonoBehaviour
{
	[SerializeField] private Transform _pointA;
	[SerializeField] private Transform _pointB;
	[SerializeField] private Transform _fill;
	[SerializeField] private float _width = 1;
	
	public IInputHandle PointA { get; private set; }
	public IInputHandle PointB { get; private set; }

	public void Start()
	{
		PointA = _pointA.GetComponent<IInputHandle>();
		PointA.Radius = _width / 2f;
		PointB = _pointB.GetComponent<IInputHandle>();
		PointB.Radius = _width / 2f;
	}

	public void Update()
	{
		// TODO: only do updates on point position change or width change.
		
		_pointA.localScale = Vector3.one * _width;
		_pointB.localScale = Vector3.one * _width;
		
		var a = _pointA.position;
		var b = _pointB.position;
		var dir = (a - b).normalized;
		var mag = (a - b).magnitude;
		
		_fill.position = (a + b) / 2f;
		_fill.rotation = Quaternion.LookRotation(Vector3.forward, dir);
		var fillScale = new Vector3(_width, mag, _width);
		_fill.localScale = fillScale;
	}
}