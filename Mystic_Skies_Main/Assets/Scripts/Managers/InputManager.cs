﻿using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
	public InputLayout defaultLayout;
	public InputLayout defaultLayoutAlt;
	public InputLayout layoutFile0;
	public InputLayout layoutFile1;
	public InputLayout layoutFile2;
	private static InputLayout[] inputLayouts;
	private static KeyCode[] keyCodes;
	private static bool acceptingInput = false;
	public static KeyCode ScreenShotCode = KeyCode.F1;
	
	void Start()
	{
		if(defaultLayout == null)
		{
			Debug.Log("[InputManager] defaultLayout is null");
		}
		if(defaultLayoutAlt == null)
		{
			Debug.Log("[InputManager] defaultLayoutAlt is null");
		}
		if(layoutFile0 == null)
		{
			Debug.Log("[InputManager] layoutFile0 is null");
		}
		if(layoutFile1 == null)
		{
			Debug.Log("[InputManager] layoutFile1 is null");
		}
		if(layoutFile2 == null)
		{
			Debug.Log("[InputManager] layoutFile2 is null");
		}
		
		// TODO: not have a hardcoded value for number of input layouts, but this implies N number of save slots tho
		inputLayouts = new InputLayout[3];
		inputLayouts[0] = layoutFile0;
		inputLayouts[1] = layoutFile1;
		inputLayouts[2] = layoutFile2;
		
		keyCodes = new KeyCode[(int)InputKeys.COUNT];
		for(int i = 0; i < keyCodes.Length; ++i)
		{
			keyCodes [i] = defaultLayout.keys[i].key;
		}
	}
	
	public static KeyCode GetKeyCode(InputKeys key)
	{
		if(acceptingInput)
		{
			return keyCodes[(int)key];
		}
		else
		{
			return KeyCode.None;
		}
	}
	
	public static KeyCode GetUIKeyCode(InputKeys key)
	{
		return keyCodes[(int)key];
	}
	
	public static void SetAcceptingInput(bool accept)
	{
		acceptingInput = accept;
	}
	public static bool IsAcceptingInput()
	{
		return acceptingInput;
	}
	
	public static void LoadGame(int fileSlotindex)
	{
		InputLayout layout = inputLayouts[fileSlotindex];
		for(int i = 0; i < keyCodes.Length; ++i)
		{
			keyCodes[i] = layout.keys[i].key;
		}
	}
}

