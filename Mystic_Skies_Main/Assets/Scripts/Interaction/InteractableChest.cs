using UnityEngine;
using System.Collections;

public class InteractableChest : Interactable
{
	// TODO: Change to ItemManager.ItemType
	public ItemManager.PotionType drop;
	private GameObject obj;
	private float yLimit;
	public float hoverDuration = 1.0f;

	void Start ()
	{
		//Create the item object and give it the position of the chest
		Vector3 InitPos = transform.position;
		obj = GameObject.Instantiate(ItemManager.GetObject (drop), InitPos, transform.rotation) as GameObject;
		obj.collider.enabled = false;

		//Turn off gravity for the item, allowing it to hover above the chest, then set the item to inactive
		obj.rigidbody.useGravity = false;
		obj.SetActive (false);

		//Create a limit of how far above the chest the item can hover
		yLimit = obj.transform.position.y + 2.0f;
	}

	void Update ()
	{
		//if the item is activated, move it above the chest and rotate it along the y-axis
		if (obj != null && obj.activeSelf == true)
		{
			if (obj.transform.position.y <= yLimit)
			{
				obj.transform.position += (Vector3.up * (2.0f * Time.deltaTime));
			}
			else
			{
				StartCoroutine("WaitThenGiftPlayer");
			}
			obj.transform.Rotate (new Vector3 (1.0f, 2.0f, 1.0f));
		}
	}

	public override void OnEnter ()
	{
		//do nothing
	}

	public override void OnInteraction ()
	{
		//if the chest hasn't been open, open it and activate the item
		if (!isInteracted)
		{
			//TODO
			//need to get an interaction state from a manager!

			if (obj != null)
			{
				print ("You have found a " + obj.name + "!");
				obj.SetActive (true);
			}

			isInteracted = true;
		}
	}

	public override void OnExit ()
	{
		//do nothing
	}

	private IEnumerator WaitThenGiftPlayer()
	{
		yield return new WaitForSeconds (hoverDuration);
		Destroy(obj);
		PlayerManager.GetPlayerScript().AddPotion(drop);
	}
}