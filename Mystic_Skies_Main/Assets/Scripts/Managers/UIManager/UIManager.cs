using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum UICanvasTypes
{
	HUD,
	Inventory,
	Pause,
	Loading,
	COUNT
}

public class UIManager : MonoBehaviour
{
	public GameObject speechBubblePrefab;
	private static GameObject speechBubbleObject;
	
	public GameObject UIImagePrefab;
	public static GameObject UIImageObject;
	
	
	public GameObject HUDCanvas;
	//public GameObject inventoryCanvas;
	public GameObject pauseCanvas;
	public GameObject dialogueBox;
	private static GameObject dialogueBoxObject;
	private static DialogueBoxController dialogueBoxController;
	
	private static GameObject[] canvases;
	private static bool[] activeCanvases;
	
	void Start()
	{
		canvases = new GameObject[(int)UICanvasTypes.COUNT];
		canvases[(int)UICanvasTypes.HUD] 		= HUDCanvas;
		//canvases[(int)UICanvasTypes.Inventory] 	= inventoryCanvas;
		canvases[(int)UICanvasTypes.Pause] 		= pauseCanvas;
		//canvases[(int)UICanvasTypes.Loading] 	= loadingCanvas;
		
		activeCanvases = new bool[(int)UICanvasTypes.COUNT];
		for(int i = 0; i < activeCanvases.Length; ++i)
		{
			activeCanvases[i] = false;
		}
		
		speechBubbleObject = speechBubblePrefab;
		dialogueBoxObject = dialogueBox;
		dialogueBoxController = dialogueBox.GetComponent<DialogueBoxController>();
		
		UIImageObject = UIImagePrefab;
	}
	
	void OnLevelWasLoaded(int level)
	{
		// TODO:
		//DeActivate (UICanvasTypes.Loading);
	}
	
	public static GameObject CreateSpeechBubble(string text, Vector3 position, Quaternion rotation, bool billboard)
	{
		GameObject speechBubble = GameObject.Instantiate (speechBubbleObject) as GameObject;
		SpeechBubbleController speechBubbleController = speechBubble.GetComponent<SpeechBubbleController>();
		
		speechBubbleController.SetText (text);
		speechBubbleController.SetPosition(position);
		speechBubbleController.SetRotation (rotation);
		speechBubbleController.SetBillboarding (billboard);
		//speechBubbleController.SetBackground ();
		
		return speechBubble;
	}
	public static GameObject CreateSpeechBubble(string text, GameObject followTarget, Vector3 followOffset, bool billboard)
	{
		GameObject speechBubble = GameObject.Instantiate (speechBubbleObject) as GameObject;
		SpeechBubbleController speechBubbleController = speechBubble.GetComponent<SpeechBubbleController>();
		
		//speechBubbleController.SetBackground ();
		speechBubbleController.SetText (text);
		speechBubbleController.SetFollowTarget (followTarget, followOffset);
		speechBubbleController.SetBillboarding (billboard);
		
		return speechBubble;
	}
	
	public static void NewDialogueBox(string text)
	{
		//		if(dialogueBoxObject.activeInHierarchy == false)
		//		{
		//			dialogueBoxObject.SetActive(true);
		//		}
		dialogueBoxObject.SetActive(true);
		dialogueBoxController.SetName ("Zalda");
		dialogueBoxController.SetText (text);
	}
	
	public static void NewDialogueBox(string name, string text)
	{
		//		if(dialogueBoxObject.activeInHierarchy == false)
		//		{
		//			dialogueBoxObject.SetActive(true);		
		//		}
		dialogueBoxObject.SetActive(true);
		dialogueBoxController.SetName (name);
		dialogueBoxController.SetText (text);
	}
	
	public static void CloseDialogueBox ()
	{
		dialogueBoxController.SetText ("");
		dialogueBoxController.SetName ("");
		dialogueBoxObject.SetActive (false);
	}
	
	public static void Activate(UICanvasTypes type)
	{
		canvases[(int)type].SetActive(true);
		activeCanvases [(int)type] = true;
	}
	public static void DeActivate(UICanvasTypes type)
	{
		canvases[(int)type].SetActive(false);
		activeCanvases [(int)type] = false;
	}
	public static void Toggle(UICanvasTypes type)
	{
		if(activeCanvases [(int)type] == true)
		{
			DeActivate(type);
		}
		if(activeCanvases [(int)type] == false)
		{
			Activate(type);
		}
	}
	
	
	
	
	
	
}

















