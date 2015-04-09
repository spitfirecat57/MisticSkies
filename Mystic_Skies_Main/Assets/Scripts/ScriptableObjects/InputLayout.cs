using UnityEngine;
using System.Collections;

[System.Serializable]
public struct NameKeycode
{
	[SerializeField]
	[HideInInspector]
	private string name;
	public KeyCode key;

	public NameKeycode(string _name)
	{
		name = _name;
		key = KeyCode.None;
	}
	public NameKeycode(string _name, KeyCode kc)
	{
		name = _name;
		key = kc;
	}
}


[System.Serializable]
public class InputLayout : ScriptableObject
{
	public NameKeycode[] keys;
}
