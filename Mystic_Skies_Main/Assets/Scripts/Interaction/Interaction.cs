using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interaction : MonoBehaviour
{
	private List<Interactable> interactablesList;
	
	void Start()
	{
		interactablesList = new List<Interactable>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if( interactablesList.Count == 0 )
		{
			interactablesList.AddRange(other.GetComponents<Interactable>());
			
			foreach(Interactable i in interactablesList)
			{
				i.OnEnter();
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		Interactable[] interactables = other.GetComponents<Interactable>();
		
		foreach(Interactable i in interactables)
		{
			if(interactablesList.Contains(i))
			{
				i.OnExit();
			}
		}
		interactablesList.Clear();
	}
	
	void Update()
	{
		if(Input.GetKeyDown(InputManager.GetUIKeyCode(InputKeys.Interact)))
		{
			foreach(Interactable i in interactablesList)
			{
				i.OnInteraction();
			}
		}
	}
}
