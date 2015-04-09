using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableSaysHello : Interactable
{
	//public List<ModalWindowData> windowData;
	private GameObject speechBubble = null;


	public override void OnEnter()
	{
	}

	public override void OnInteraction()
	{
		if(!isInteracted)
		{
			if(speechBubble == null)
			{
				print ("[RequestsASpeechBubble] Requesting speech bubble");
				speechBubble = UIManager.CreateSpeechBubble("Hello World", gameObject, Vector3.up * 2.0f, true);
			}
			else
			{
				SpeechBubbleController speechBubbleController = speechBubble.GetComponent<SpeechBubbleController>();
				speechBubbleController.SetText("Hello again");
			}
			isInteracted = true;
		}
	}

	public override void OnExit()
	{
		if(speechBubble)
		{
			Destroy(speechBubble);
			speechBubble = null;
		}
		isInteracted = false;
	}





}
