using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OnInputKey_UI_ReturnToCanvas : OnInputKeyEvent
{
	public GameObject returnToCanvas;

	void Update()
	{
		if(Input.GetKeyDown(InputManager.GetUIKeyCode(InputKeys.Exit)))
		{
			returnToCanvas.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
