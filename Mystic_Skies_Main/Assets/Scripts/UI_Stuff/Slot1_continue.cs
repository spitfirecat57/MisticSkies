using UnityEngine;
using System.Collections;

public class Slot1_continue : MonoBehaviour
{
	public int saveFileIndex = 0;
	
	void OnMouseDown()
	{
		GameManager.LoadGame(saveFileIndex);
	}
}
