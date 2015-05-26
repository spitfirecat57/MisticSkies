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
	public float health;
	public float maxHealth;
	public float mana;
	public float maxMana;
	public float manaRegen;
	private float manaRegenTimer = 0.0f;
	public float toughnes;
	public float speedEpsilon;
	public float friction;
	public float maxTargetingRange;
	public float mKnockbackHeight = 4.0f;
	public List<NameValue> potions;
	public List<NameValue> inventory;
	public List<NameValue> collectibles;

	private bool isInvincible = false;
	private bool knockingBack = false;
	
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

		manaRegenTimer += Time.deltaTime;
		if(manaRegenTimer > 3.0f)
		{
			IncreaseMana(maxMana * manaRegen);
			manaRegenTimer = 0.0f;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Floor"))
		{
			knockingBack = false;
		}
	}
	
	public void TakeDamage(float damage)
	{
		if (isInvincible) return;

		health -= damage * (1.0f - toughnes);
		
		if(health <= 0.0f)
		{
			// play animation?
			
			// go to death screen?
			
			print("Player died.");
		}
	}
	
	public void KnockBack(Vector3 knockback)
	{
		if (isInvincible) return;
		rigidbody.velocity = new Vector3(knockback.x, mKnockbackHeight, knockback.z);
		knockingBack = true;
		StartCoroutine("CoKnockBack", knockback);
	}
	
	IEnumerator CoKnockBack(Vector3 knockback)
	{
		mc.enabled = false;
		while(knockingBack)
		{
			rigidbody.velocity *= (1.0f - (friction * Time.deltaTime));
			yield return null;
		}
		mc.enabled = true;
		rigidbody.velocity = Vector3.zero;	
	}
	
	public void IncreaseHealth(float val)
	{
		health += val;
		if(health > maxHealth)
		{
			health = maxHealth;
		}
		if(health < 0.0f)
		{
			health = 0.0f;
		}
	}
	
	public void IncreaseMana(float val)
	{
		mana += val;
		if(mana > maxMana)
		{
			mana = maxMana;
		}
		if(mana < 0.0f)
		{
			mana = 0.0f;
		}
	}

	public void IncreaseMaxHealth(float val)
	{
		maxHealth *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMaxMana(float val)
	{
		maxMana *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMagicRegen(float val)
	{
		manaRegen += val / 100.0f;
	}
	public void IncreaseStrength(float val)
	{
		PlayerManager.GetPlayerSpellController().IncreaseStrength(val);
	}
	public void IncreaseToughness(float val)
	{
		toughnes += val;
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

	public void SetInvincible(bool invincible)
	{
		isInvincible = invincible;
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






