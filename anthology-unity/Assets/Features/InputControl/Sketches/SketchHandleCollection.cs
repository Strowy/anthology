using System.Collections.Generic;

public class SketchHandleCollection : IInputHandleCollection
{
	public IEnumerable<IInputHandle> AvailableHandles => _handles;

	private readonly List<IInputHandle> _handles = new();

	public void Clear()
	{
		_handles.Clear();
	}

	public void Add(IInputHandle handle)
	{
		_handles.Add(handle);
	}

	public void Remove(IInputHandle handle)
	{
		_handles.Remove(handle);
	}
}