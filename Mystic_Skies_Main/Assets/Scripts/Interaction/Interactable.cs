using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
	public bool resetOnLoad = true;
	protected bool isInteracted = false;
	
	public abstract void OnEnter();
	public abstract void OnInteraction();
	public abstract void OnExit();
	
	void Start()
	{
		if(!resetOnLoad)
		{
			isInteracted = SceneManager.GetWasInteracted(name);
		}
	}
	
	void OnDestroy()
	{
		if(!resetOnLoad)
		{
			SceneManager.SetWasInteracted(name, isInteracted);
		}
	}
}
