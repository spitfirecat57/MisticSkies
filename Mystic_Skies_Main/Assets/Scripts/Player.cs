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
	public float fireMana;
	public float maxFireMana;
	public float waterMana;
	public float maxWaterMana;
	public float rockMana;
	public float maxRockMana;
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

	Animator anim;

	
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
			IncreaseFireMana(maxFireMana * manaRegen);
			IncreaseWaterMana(maxWaterMana * manaRegen);
			IncreaseRockMana(maxRockMana * manaRegen);
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

			Animator anim = GetComponentInChildren<Animator>();
				anim.CrossFade ("Dead", 0f);
			//anim.SetBool("Dead", true);
			health = 0.0f;
			GameManager.LoadCurrentGameStatic();
			//
		//	Animator anim = GetComponentInChildren<Animator>();
		//	anim.CrossFade ("Dead", 0f);
		//	anim.SetBool("Dead", true);
			//
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
	
	public void IncreaseFireMana(float val)
	{
		fireMana += val;
		if(fireMana > maxFireMana)
		{
			fireMana = maxFireMana;
		}
		if(fireMana < 0.0f)
		{
			fireMana = 0.0f;
		}
	}

	public void IncreaseWaterMana(float val)
	{
		waterMana += val;
		if(waterMana > maxWaterMana)
		{
			waterMana = maxWaterMana;
		}
		if(waterMana < 0.0f)
		{
			waterMana = 0.0f;
		}
	}

	public void IncreaseRockMana(float val)
	{
		rockMana += val;
		if(rockMana > maxRockMana)
		{
			rockMana = maxRockMana;
		}
		if(rockMana < 0.0f)
		{
			rockMana = 0.0f;
		}
	}

	public void IncreaseMaxHealth(float val)
	{
		maxHealth *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMaxFireMana(float val)
	{
		maxFireMana *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMaxWaterMana(float val)
	{
		maxWaterMana *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMaxRockMana(float val)
	{
		maxRockMana *= 1.0f + (val / 100.0f);
	}
	public void IncreaseMagicRegen(float val)
	{
		manaRegen += val;
	}
	public void IncreaseStrength(float val)
	{
		PlayerManager.GetPlayerSpellController().IncreaseStrength(val);
	}
	public void IncreaseToughness(float val)
	{
		toughnes += val / 100.0f;
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
				IncreaseFireMana(ItemManager.GetValue(type));
				IncreaseWaterMana(ItemManager.GetValue(type));
				IncreaseRockMana(ItemManager.GetValue(type));
				break;
			}
			case ItemManager.PotionType.Rejuv:
			{
				IncreaseHealth(ItemManager.GetValue(type));
				IncreaseFireMana(ItemManager.GetValue(type));
				IncreaseWaterMana(ItemManager.GetValue(type));
				IncreaseRockMana(ItemManager.GetValue(type));
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
		fireMana = data.fireMana;
		maxFireMana = data.maxFireMana;
		waterMana = data.waterMana;
		maxWaterMana = data.maxWaterMana;
		rockMana = data.rockMana;
		maxRockMana = data.maxRockMana;
		print ("[Player] Saving: " +
		       "health = " + health +
		       "maxFireMana = " + maxFireMana);
		
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
		data.fireMana = fireMana;
		data.maxFireMana = maxFireMana;
		data.waterMana = waterMana;
		data.maxWaterMana = maxWaterMana;
		data.rockMana = rockMana;
		data.maxRockMana = maxRockMana;

		print ( "[Player] Saving: " +
				"health = " + data.health +
		        "maxFireMana = " + data.maxFireMana);
		
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






