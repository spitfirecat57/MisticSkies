using UnityEngine;
using System.Collections;

public class OnInputKey_Application_Quit : OnInputKeyEvent
{
	void Update()
	{
		if(Input.GetKeyDown(InputManager.GetUIKeyCode(InputKeys.Exit)))
		{
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit;
			#endif
		}
	}
}
