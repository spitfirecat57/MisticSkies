using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour
{
	private GameObject panelObject;
	private bool isPanelActive;

	void Awake()
	{
		panelObject = gameObject;
		//panelObject.SetActive(false);
		//isPanelActive = false;

		// TODO: remove this, default to deactivated
		isPanelActive = true;
	}

	public void Activate()
	{
		// moves to the front
		transform.SetAsLastSibling ();
		panelObject.SetActive(true);
		isPanelActive = true;
	}

	public void Deactivate()
	{
		panelObject.SetActive(false);
		isPanelActive = false;
	}

	public bool IsPanelActive()
	{
		return isPanelActive;
	}
}
