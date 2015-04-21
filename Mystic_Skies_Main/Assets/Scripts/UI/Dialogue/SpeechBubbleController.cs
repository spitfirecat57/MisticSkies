using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour
{
	public Canvas bubbleCanvas;
	private RectTransform rectTransform;
	public Image background;
	public Text text;

	private GameObject followTarget = null;
	private Vector3 followOffset;
	private bool isBillboarding = true;

	public Sprite backgroundDefault;
	public string textDefault;

	void Start()
	{
		bubbleCanvas = gameObject.GetComponent<Canvas>();
		rectTransform = (RectTransform)(gameObject.transform);
		background = gameObject.GetComponentInChildren<Image>();
		text = gameObject.GetComponentInChildren<Text>();

		if(backgroundDefault != null)
		{
			background.sprite = backgroundDefault;
		}
		if(textDefault.Length > 0)
		{
			text.text = textDefault;
		}
	}

	void Update()
	{
		if(followTarget != null)
		{
			transform.position = followTarget.transform.position + followOffset;
			transform.rotation = followTarget.transform.rotation;
		}
		if(isBillboarding)
		{
			transform.LookAt(Camera.main.transform);
			transform.forward *= -1.0f;
		}
	}

	public void SetPosition(Vector3 pos)
	{
		rectTransform.position = pos;
	}

	public void SetRotation(Quaternion rot)
	{
		rectTransform.rotation = rot;
	}

	public void SetSize(Vector2 size)
	{
		rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, size.x);
		rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, size.y);
	}

	public void SetFollowTarget(GameObject obj, Vector3 offset)
	{
		followTarget = obj;
		followOffset = offset;
		transform.position = obj.transform.position + offset;
	}

	public void SetBillboarding(bool billboarding)
	{
		isBillboarding = billboarding;
		if(isBillboarding)
		{
			transform.LookAt(Camera.main.transform);
			transform.forward *= -1.0f;
		}
	}

	public void SetBackground(Sprite sprite)
	{
		background.sprite = sprite;
	}

	public void SetText(string txt)
	{
		text.text = txt;
	}
}
