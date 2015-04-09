using UnityEngine;
using System.Collections;

public class InventoryItem : MonoBehaviour
{
	public ItemManager.InventoryItemType type;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Floor"))
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.Sleep();
		}
		if(other.CompareTag("Player"))
		{
			Player player = other.GetComponent<Player>();
			player.AddInventoryItem(type);

			print ("[InventoryItem] Player picked up a " + type.ToString());
			
			Destroy(gameObject);
		}		
	}
}
