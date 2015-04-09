using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OnInputKey_UI_ReturnToCanvas : OnInputKeyEvent
{
	public GameObject returnToCanvas;
	private CanvasController otherController;
	private CanvasController myController;

	void Start()
	{
		myController = GetComponent<CanvasController> ();
		otherController = returnToCanvas.GetComponent<CanvasController> ();
	}

	void Update()
	{
		if(Input.GetKeyDown(InputManager.GetUIKeyCode(InputKeys.Exit)))
		{
			otherController.Activate();
			myController.DeActivate();
		}
	}
}
