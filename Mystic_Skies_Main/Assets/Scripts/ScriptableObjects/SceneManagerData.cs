using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ============================================================
// struct used in ObjectStates scriptable object
[System.Serializable]
public struct ObjectState
{
	public string name;
	public bool state;
	
	public ObjectState(string name, bool state)
	{
		this.name = name;
		this.state = state;
	}
	
	public bool IsValid()
	{
		if(this.name == "")
		{
			return false;
		}
		return true;
	}
}


// ============================================================
// scriptable object of list of 'String to Bool' structs
[System.Serializable]
public class SceneManagerData : ScriptableObject
{
	public List<ObjectState> objectStates;
	
	public Scenes currentScene;
	public int transitionPointIndex;
}

