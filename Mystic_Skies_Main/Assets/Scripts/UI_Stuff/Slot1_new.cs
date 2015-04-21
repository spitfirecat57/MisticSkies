using UnityEngine;
using System.Collections;

public class Slot1_new : MonoBehaviour
{
	public int saveFileIndex = 0;
	
	void OnMouseDown()
	{
		GameManager.StartNewGameStatic();
	}
}
