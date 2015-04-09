using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveFileInfoText : MonoBehaviour
{
	public int saveFileIndex;
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		if(saveFileIndex < 0 || saveFileIndex > 2)
		{
			Debug.Log("[SaveFileInfoText] index out of range!!!");
		}
		else
		{
			text.text = string.Format("Name: Testy\n" +
			                          "Location: " + SceneManager.GetSaveFileScene(saveFileIndex));;
		}
	}
}
