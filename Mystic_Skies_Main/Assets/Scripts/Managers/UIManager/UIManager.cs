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

public class UIManager : Singleton<UIManager>
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
	private static CanvasController[] canvasControllers;
	private static UICanvasTypes activeCanvasType;
	
	void Start()
	{
		canvases = new GameObject[(int)UICanvasTypes.COUNT];
		canvases[(int)UICanvasTypes.HUD] 		= HUDCanvas;
		//canvases[(int)UICanvasTypes.Inventory] 	= inventoryCanvas;
		canvases[(int)UICanvasTypes.Pause] 		= pauseCanvas;
		//canvases[(int)UICanvasTypes.Loading] 	= loadingCanvas;


		canvasControllers = new CanvasController[(int)UICanvasTypes.COUNT];
		canvasControllers[(int)UICanvasTypes.HUD] 				= HUDCanvas.GetComponent<CanvasController>();
		//canvasControllers[(int)UICanvasTypes.Inventory] 		= inventoryCanvas.GetComponent<CanvasController>();
		canvasControllers[(int)UICanvasTypes.Pause] 			= pauseCanvas.GetComponent<CanvasController>();
		//		canvasControllers[(int)UICanvasTypes.Pause] 	= loadingCanvas.GetComponent<CanvasController>();


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
		if(dialogueBoxObject.activeInHierarchy == false)
		{
			dialogueBoxObject.SetActive(true);
		}
		dialogueBoxController.SetName ("Kessho");
		dialogueBoxController.SetText (text);
	}

	public static void NewDialogueBox(string name, string text)
	{
		if(dialogueBoxObject.activeInHierarchy == false)
		{
			dialogueBoxObject.SetActive(true);		
		}
		dialogueBoxController.SetName (name);
		dialogueBoxController.SetText (text);
	}

	public static void CloseDialogueBox ()
	{
		dialogueBoxController.SetText ("");
		dialogueBoxController.DeActivate();
	}
	
	public static void Activate(UICanvasTypes type)
	{
		canvasControllers[(int)type].Activate();
		activeCanvasType = type;
	}
	public static void DeActivate(UICanvasTypes type)
	{
		print ("De actvate canvas type '" + type.ToString() + "'");
		canvasControllers[(int)type].DeActivate();
		if(type != UICanvasTypes.HUD)
		{
			activeCanvasType = UICanvasTypes.HUD;
		}
	}
	
	
	public static void SwitchTo(UICanvasTypes type)
	{
		canvasControllers[(int)activeCanvasType].DeActivate();
		activeCanvasType = type;
		canvasControllers[(int)activeCanvasType].Activate();
	}
	
	
	
	
}

















