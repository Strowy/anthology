using System.Collections.Generic;
using UnityEngine;

public interface IInputControlHandler
{
	Vector3 WorldPosition { get; }
	bool Handling { get; }
	IInputHandle Held { get; }
}

public interface IInputHandle
{
	Vector3 WorldPosition { get; }
	float Radius { get; }
	void MoveTo(Vector3 position);
}

public interface IInputHandleCollection
{
	IEnumerable<IInputHandle> AvailableHandles { get; }
}