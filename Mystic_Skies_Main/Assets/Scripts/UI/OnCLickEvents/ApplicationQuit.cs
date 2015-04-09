using UnityEngine;
using System.Collections;

public class ApplicationQuit : OnCLickEvent
{
	private void QuitEditor()
	{
		UnityEditor.EditorApplication.isPlaying = false;
	}

	void Awake()
	{
#if UNITY_EDITOR
		onClickEvent = QuitEditor;
#else
		onClickEvent = Application.Quit;
#endif
	}
}
