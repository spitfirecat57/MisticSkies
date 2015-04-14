using UnityEngine;
using System.Collections;
using System;


public class DialogueDriver : Interactable
{
	[System.Serializable]
	public struct ActorLine
	{
		public string name;
		public string line;
	}

	//public ActorLine[] actors;
	public ActorLine[] dialogue;
	private int dialogueIndex = 0;
	private int numLines = 0;


	void Start()
	{
		numLines = dialogue.Length;
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

		if(dialogueIndex < numLines)
		{
		
			//UIManager.NewDialogueBox(actors[dialogue[dialogueIndex].id].line, dialogue[dialogueIndex].line);
			UIManager.NewDialogueBox(dialogue[dialogueIndex].name, dialogue[dialogueIndex].line);
			dialogueIndex++;
		}
		else
		{
			InputManager.SetAcceptingInput(true);
			UIManager.CloseDialogueBox ();
			isInteracted = false;
		}

	}

	public override void OnExit()
	{
	}





	
//	void Update()
//	{
//		if(isPlaying && Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.Interact)))
//		{
//			if(speakingOrderIndex == speakingOrder.Length)
//			{
//				StopAndReset();
//				return;
//			}
//
//			ActorSpeak(speakingOrder[speakingOrderIndex++]);
//		}
//	}
//
//
//	
//	private void ActorSpeak(int actorIndex)
//	{
//		Destroy (currentSpeechBubble);
//		// get bubble from uimanager
//		currentSpeechBubble = UIManager.CreateSpeechBubble (dialogueLists [actorIndex] [actorDialogueIndices[actorIndex]], actors [actorIndex], followOffsetDefault, true);
//		// increment actor's dialogue index
//		actorDialogueIndices [actorIndex] = (actorDialogueIndices [actorIndex] + 1) % dialogueLists [actorIndex].Length;
//	}


}










