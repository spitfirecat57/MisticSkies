using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxController : CanvasController
{
	private Image background = null;
	public Text textbox;
	public Text namebox;

	public void SetBackground(Sprite sprite)
	{
		background.sprite = sprite;
	}

	public void SetText(string txt)
	{
		textbox.text = txt;
	}

	public void SetName(string name)
	{
		namebox.text = name;
	}
}
