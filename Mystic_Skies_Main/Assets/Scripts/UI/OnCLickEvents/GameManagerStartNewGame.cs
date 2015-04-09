using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManagerStartNewGame : MonoBehaviour
{
	public int saveFileIndex = 0;
	

	void OnMouseDown()
	{
		GameManager.StartNewGame (saveFileIndex);
	}

}
