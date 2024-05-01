using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShapeView : MonoBehaviour
{
	[SerializeField] private int _vertices = 3;
	[SerializeField] private float _initRadius = 1;
	[SerializeField] private LineView _linePrefab;

	private readonly List<LineView> _edges = new();

	public event Action OnGenerate;
	
	public List<IInputHandle> Handles { get; } = new();

	public void OnValidate()
	{
		if (_vertices < 3) _vertices = 3;
	}

	public void Update()
	{
		if (_edges.Count != _vertices) GenerateEdges();

		for (var i = 0; i < _edges.Count; i++)
			_edges[i].End.position = _edges[(i + 1) % _edges.Count].Origin.position;
	}

	public float2[] GetVertices()
	{
		var vertices = new float2[_edges.Count];
		for (var i = 0; i < vertices.Length; i++)
		{
			vertices[i] = _edges[i].Origin.transform.position.ToFloat2();
		}

		return vertices;
	}


	private void GenerateEdges()
	{
		foreach (var edge in _edges)
			Destroy(edge.gameObject);

		_edges.Clear();
		Handles.Clear();
		var vertices = ShapeGeometry.GetRegularVertices(_vertices, _initRadius);

		for (var i = 0; i < _vertices; i++)
		{
			var lineView = Instantiate(_linePrefab, transform);
			_edges.Add(lineView);
			Handles.Add(lineView.OriginHandle);
			lineView.Model = new CapsuleModel
			{
				Edge = new EdgeModel
				{
					Origin = vertices[i],
					End = vertices[(i + 1) % _vertices],
				},
				Radius = 0.1f,
			};
		}
		
		OnGenerate?.Invoke();
	}
}