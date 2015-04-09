using UnityEngine;
using System.Collections;

public class Consumable : MonoBehaviour
{
	public ItemManager.ConsumableType type;

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

			if(player)
			{
				switch(type)
				{
					case ItemManager.ConsumableType.HealthShard:
					{
						print ("ItemType = Consumable.Heart");
						player.IncreaseHealth(ItemManager.GetValue(type));
						break;
					}
					case ItemManager.ConsumableType.PotionHealth:
					{
						print ("ItemType = Consumable.PotionHealth");
						player.AddPotion(ItemManager.PotionType.Health);
						break;
					}
					case ItemManager.ConsumableType.PotionMana:
					{
						print ("ItemType = Consumable.PotionMana");
						player.AddPotion(ItemManager.PotionType.Mana);
						break;
					}
					case ItemManager.ConsumableType.PotionRejuv:
					{
						print ("ItemType = Consumable.PotionRejuv");
						player.AddPotion(ItemManager.PotionType.Rejuv);
						break;
					}
				}
				
				Destroy(gameObject);
			}
		}

	}

//	void OnCollisionEnter(Collision collision)
//	{
//		if(collision.collider.CompareTag("Floor"))
//		{
//			rigidbody.velocity = Vector3.zero;
//			//rigidbody.isKinematic = true;
//			rigidbody.Sleep();
//		}
//	}
}
