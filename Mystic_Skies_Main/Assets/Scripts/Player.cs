using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NameValue
{
	[SerializeField]
	[HideInInspector]
	private string name;
	public int value;
	public NameValue(string _name, int val)
	{
		name = _name;
		value = val;
	}
}

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	public int health;
	public int maxHealth;
	public int mana;
	public int maxMana;
	public float speedEpsilon;
	public float friction;
	public List<NameValue> potions;
	public List<NameValue> inventory;
	public List<NameValue> collectibles;
	
	private PlayerController mc;
	
	
	void Start()
	{
		if(PlayerManager.GetPlayerObject() != gameObject)
		{
			if(transform.parent)
			{
				Destroy(transform.parent.gameObject);
			}
			else
			{
				GameObject[] cams = GameObject.FindGameObjectsWithTag("MainCamera");
				foreach(GameObject cam in cams)
				{
					if(PlayerManager.GetCameraObject() != cam)
					{
						Destroy(cam);
					}
				}
				Destroy(gameObject);
			}
		}
		
		potions = new List<NameValue> ();
		for(int i = 0; i < (int)ItemManager.PotionType.COUNT; ++i)
		{
			NameValue n = new NameValue( ItemManager.GetName((ItemManager.PotionType)i), 0);
			potions.Add(n);
		}
		inventory = new List<NameValue> ();
		for(int i = 0; i < (int)ItemManager.InventoryItemType.COUNT; ++i)
		{
			NameValue n = new NameValue( ItemManager.GetName((ItemManager.InventoryItemType)i), 0);
			inventory.Add(n);
		}
		collectibles = new List<NameValue> ();
		for(int i = 0; i < (int)ItemManager.CollectibleType.COUNT; ++i)
		{
			NameValue n = new NameValue( ItemManager.GetName((ItemManager.CollectibleType)i), 0);
			collectibles.Add(n);
		}
		
		mc = GetComponent<PlayerController>();
	}
	
	void Update()
	{
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.HealthPotion)))
		{
			ConsumePotion(ItemManager.PotionType.Health);
		}
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.ManaPotion)))
		{
			ConsumePotion(ItemManager.PotionType.Mana);
		}
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.RejuvPotion)))
		{
			ConsumePotion(ItemManager.PotionType.Rejuv);
		}
	}
	
	public void TakeDamage(int damage)
	{
		health -= damage;
		
		if(health < 1)
		{
			// play animation?
			
			// go to death screen?
			
			print("Player died.");
		}
	}
	
	public void KnockBack(Vector3 knockback)
	{
		StartCoroutine("CoKnockBack", knockback);
	}
	
	IEnumerator CoKnockBack(Vector3 knockback)
	{
		mc.enabled = false;
		rigidbody.velocity = new Vector3(knockback.x, 8.0f, knockback.z);
		while(gameObject.rigidbody.velocity.sqrMagnitude > speedEpsilon)
		{
			rigidbody.velocity *= (1.0f - friction);
			yield return new WaitForEndOfFrame();
		}
		rigidbody.velocity = Vector3.zero;
		mc.enabled = true;
		
	}
	
	public void IncreaseHealth(int val)
	{
		health += val;
		if(health > maxHealth)
		{
			health = maxHealth;
		}
		if(health < 0)
		{
			health = 0;
		}
	}
	
	public void IncreaseMana(int val)
	{
		mana += val;
		if(mana > maxMana)
		{
			mana = maxMana;
		}
		if(mana < 0)
		{
			mana = 0;
		}
	}
	
	
	public void AddPotion(ItemManager.PotionType type)
	{
		++potions[(int)type].value;
	}
	
	public void AddInventoryItem(ItemManager.InventoryItemType type)
	{
		++inventory[(int)type].value;
	}
	
	public void AddCollectibleItem(ItemManager.CollectibleType type)
	{
		++collectibles[(int)type].value;
	}
	
	private void ConsumePotion(ItemManager.PotionType type)
	{
		if(potions[(int)type].value > 0)
		{
			--potions[(int)type].value;
			switch(type)
			{
			case ItemManager.PotionType.Health:
			{
				IncreaseHealth(ItemManager.GetValue(type));
				break;
			}
			case ItemManager.PotionType.Mana:
			{
				IncreaseMana(ItemManager.GetValue(type));
				break;
			}
			case ItemManager.PotionType.Rejuv:
			{
				IncreaseHealth(ItemManager.GetValue(type));
				IncreaseMana(ItemManager.GetValue(type));
				break;
			}
			}
		}
	}
	
	
	public void LoadData(PlayerData data)
	{
		health = data.health;
		maxHealth = data.maxHealth;
		mana = data.mana;
		maxMana = data.maxMana;
		
		for(int i = 0; i < potions.Count; ++i)
		{
			potions[i].value = data.potions[i];
		}
		for(int i = 0; i < inventory.Count; ++i)
		{
			inventory[i].value = data.inventory[i];
		}
		for(int i = 0; i < collectibles.Count; ++i)
		{
			collectibles[i].value = data.collectibles[i];
		}
	}
	
	
	public void SaveData(PlayerData data)
	{
		data.health = health;
		data.maxHealth = maxHealth;
		data.mana = mana;
		data.maxMana = maxMana;
		
		for(int i = 0; i < potions.Count; ++i)
		{
			data.potions[i] = potions[i].value;
		}
		for(int i = 0; i < inventory.Count; ++i)
		{
			data.inventory[i] = inventory[i].value;
		}
		for(int i = 0; i < collectibles.Count; ++i)
		{
			data.collectibles[i] = collectibles[i].value;
		}
	}
	
	
	
	
}






