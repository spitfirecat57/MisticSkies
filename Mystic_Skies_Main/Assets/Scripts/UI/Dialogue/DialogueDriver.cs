//using UnityEngine;
//using System.Collections;
//using System;
//
//public class DialogueDriver : MonoBehaviour
//{
//	// TODO: isCinematic
//	//public bool isCinematic = false;
//	//public bool includesPlayer = true;
//	// exitDist = 0 means no exit
//	//public float exitDist = 0.0f;
//
//	//public GameObject[] actors;
//
//	public string[] actor1_Dialogue;
//	public string[] actor2_Dialogue;
//	private int actor1_DialogueIndex;
//	private int actor2_DialogueIndex;
//
//	public int[] speakingOrder;
//	private int speakingOrderIndex = 0;
//	
//	private bool isPlaying = false;
//
//	void Start()
//	{
//		// TODO: [Release]Remove debug checks
//
//		// check number of dialogue lists
//		if(actor1_Dialogue.Length == 0)
//		{
//			Debug.Log("[DialogueDriver] GameObject '" + gameObject.name + " actor1 has no dialogue!!!");
//		}
//		if(actor2_Dialogue.Length == 0)
//		{
//			Debug.Log("[DialogueDriver] GameObject '" + gameObject.name + " actor2 has no dialogue!!!");
//		}
//
//		// Check number of dialogue lists against number of actors speaking
//		if(speakingOrder.Length > (actor1_Dialogue.Length + actor2_Dialogue.Length))
//		{
//			Debug.Log("[DialogueDriver] GameObject '" + gameObject.name + " has more speaking than!!!");
//		}
//
//	}
//
//
//
//	public void StartConversation()
//	{
//		ActorSpeak(speakingOrder[speakingOrderIndex++]);
//		isPlaying = true;
//	}
//
//
//
//	public void StopAndReset()
//	{
//		speakingOrderIndex = 0;
//		for(int i = 0; i < actorDialogueIndices.Length; ++i)
//		{
//			actorDialogueIndices[i] = 0;
//		}
//		isPlaying = false;
//	}
//
//
//	
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
//
//
//}
//
//
//
//
//
//
//
//
//
//
