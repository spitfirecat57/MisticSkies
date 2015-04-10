using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueBoxController : CanvasController
{
	private Image background = null;
	public Text textbox;

	public Sprite backgroundDefault;
	public string textDefault;

	void Start()
	{
		background = gameObject.GetComponentInChildren<Image>();
		//textbox = gameObject.GetComponentInChildren<Text>();
		if(textbox == null)
		{
			Debug.Log("[DialogueBoxController] could not find Text object in children");
		}

		if(backgroundDefault != null)
		{
			background.sprite = backgroundDefault;
		}
		if(textDefault.Length > 0)
		{
			textbox.text = textDefault;
		}
	}

	public void SetBackground(Sprite sprite)
	{
		background.sprite = sprite;
	}

	public void SetText(string txt)
	{
		if(textbox)
		{
			textbox.text = txt;
		}
		else
		{
			Debug.Log("[DialogueBoxController] textbox is null");
		}
	}
}
