using UnityEngine;

public class DiscView : MonoBehaviour
{
	[SerializeField] private float _radius = 0.5f;

	public IInputHandle Handle { get; private set; }

	public DiscModel Model
	{
		get => ViewToModel();
		set => UpdateModel(value);
	}

	public void Start()
	{
		Handle = GetComponent<IInputHandle>();
		Handle.Radius = _radius;
		Refresh();
	}

	public void Update()
	{
		Refresh();
	}

	private void Refresh()
	{
		transform.localScale = Vector3.one * _radius * 2;
	}

	private void UpdateModel(DiscModel model)
	{
		transform.position = model.Position.ToVector3();
		_radius = model.Radius;
		transform.localScale = Vector3.one * model.Radius * 2;
	}

	private DiscModel ViewToModel()
	{
		return new DiscModel
		{
			Position = transform.position.ToFloat2(),
			Radius = _radius,
		};
	}
}