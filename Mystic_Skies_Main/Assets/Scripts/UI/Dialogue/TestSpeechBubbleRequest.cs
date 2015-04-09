using UnityEngine;
using System.Collections;

public class TestSpeechBubbleRequest : MonoBehaviour
{
	private GameObject speechBubble = null;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(speechBubble == null)
			{
				print ("[RequestsASpeechBubble] Requesting speech bubble");
			 	speechBubble = UIManager.CreateSpeechBubble("Hello World", gameObject, Vector3.up * 2.0f, true);
			}
			else
			{
				SpeechBubbleController speechBubbleController = speechBubble.GetComponent<SpeechBubbleController>();
				speechBubbleController.SetText("Hello again");
			}
		}

		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			if(speechBubble)
			{
				Destroy(speechBubble);
				speechBubble = null;
			}
		}
	}
}
