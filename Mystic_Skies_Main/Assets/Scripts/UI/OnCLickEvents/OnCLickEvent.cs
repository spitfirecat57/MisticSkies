using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class OnCLickEvent : MonoBehaviour
{
	protected UnityAction onClickEvent;

	void Start()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener (onClickEvent);
	}
}
