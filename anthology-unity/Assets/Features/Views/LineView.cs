using UnityEngine;

public class LineView : MonoBehaviour
{
	[SerializeField] private Transform _pointA;
	[SerializeField] private Transform _pointB;
	[SerializeField] private Transform _fill;
	[SerializeField] private float _width = 1;

	public IInputHandle Origin { get; private set; }
	public IInputHandle End { get; private set; }

	public CapsuleModel Model
	{
		get => ViewToModel();
		set => UpdateModel(value);
	}

	public void Start()
	{
		Origin = _pointA.GetComponent<IInputHandle>();
		Origin.Radius = _width / 2;
		End = _pointB.GetComponent<IInputHandle>();
		End.Radius = _width / 2;

		Refresh();
	}

	public void Update()
	{
		Refresh();
	}

	private void Refresh()
	{
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

	private void UpdateModel(CapsuleModel model)
	{
		_pointA.position = model.Edge.Origin.ToVector3();
		_pointB.position = model.Edge.End.ToVector3();
		_width = model.Radius * 2;
		Refresh();
	}

	private CapsuleModel ViewToModel()
	{
		return new CapsuleModel
		{
			Edge = new EdgeModel
			{
				Origin = _pointA.position.ToFloat2(),
				End = _pointB.position.ToFloat2(),
			},
			Radius = _width / 2f,
		};
	}
}