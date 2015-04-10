using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableSaysHello : Interactable
{
	//public List<ModalWindowData> windowData;
	//private GameObject speechBubble = null;
	private string[] thingsToSay;
	private const int numThingsToSay = 4;
	private int indexOfWhatToSay = 0;

	void Start()
	{
		thingsToSay = new string[numThingsToSay];
		thingsToSay [0] = "First thing";
		thingsToSay [1] = "Second thing";
		thingsToSay [2] = "One More Thing";
		thingsToSay [3] = "Last Thing";
	}


	public override void OnEnter()
	{
	}

	public override void OnInteraction()
	{
		if(!isInteracted)
		{
			InputManager.SetAcceptingInput(false);
			isInteracted = true;
		}

		UIManager.NewDialogueBox(thingsToSay[indexOfWhatToSay]);
		indexOfWhatToSay = (indexOfWhatToSay + 1) % numThingsToSay;

		if(indexOfWhatToSay == 0)
		{
			InputManager.SetAcceptingInput(true);
			UIManager.CloseDialogueBox ();
			isInteracted = false;
		}
	}

	public override void OnExit()
	{
		isInteracted = false;
	}
	



}
