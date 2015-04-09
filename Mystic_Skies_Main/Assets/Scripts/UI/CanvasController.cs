using UnityEngine;
using System.Collections;

// Base class for a class that handles a UI Canvas, used by UIManager
public class CanvasController : MonoBehaviour
{
	public void Activate()
	{
		gameObject.SetActive (true);
	}

	public void DeActivate()
	{
		gameObject.SetActive (false);
	}
}
