using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
	//######################################################################
	// ENUM ItemType : ALL ITEMS IN THE GAME

	private enum ItemType
	{
		// ##### CONSUMABLES #####
		// Potions must come first, trust me
		PotionHealth,
		PotionMana,
		PotionRejuv,
		HealthShard,
		MaxHealthShard,
		MaxMagicShard,
		MagicRegenShard,
		StrengthShard,
		ToughnessShard,
		// ##### INVENTORY ITEMS #####
		Wand,
		// ##### COLLECTIBLES #####
		PuzzlePiece,
		COUNT
	};


	//######################################################################
	// ENUM ConsumableType: ANYTHING THAT CAN BE PICKED UP BY THE PLAYER

	public enum ConsumableType
	{
		// NOTE: this must mirror ItemType list
		PotionHealth,
		PotionMana,
		PotionRejuv,
		HealthShard,
		MaxHealthShard,
		MaxMagicShard,
		MagicRegenShard,
		StrengthShard,
		ToughnessShard,
		COUNT
	};


	//######################################################################
	// STRUCT DropChance: tuneable values for when enemies die

	[System.Serializable]
	public struct ConsumableDropChance
	{
		// percentChanceToDrop is chance to drop SOMETHING (out of 100).
		public float percentChanceToDrop;
		//Other values are ratios for WHAT TO DROP.
		public float healthShardRatio;
		public float potionHealthRatio;
		public float potionManaRatio;
		public float potionRejuvRatio;
	};


	//######################################################################
	// ENUM PotionType: THE POTIONS

	public enum PotionType
	{
		Health,
		Mana,
		Rejuv,
		COUNT
	};


	//######################################################################
	// ENUM InventoryItemType: ITEMS FOUND IN THE INVENTORY (EQUIPPABLE, USEABLE, ETC.)

	public enum InventoryItemType
	{
		Wand,
		COUNT
	};


	//######################################################################
	// ENUM CollectibleType: THINGS THAT AREN'T ACTIVELY USED BY THE PLAYER (QUEST ITEMS, COLLECTIBLES, ETC.)

	public enum CollectibleType
	{
		PuzzlePiece,	
		COUNT
	};


	//######################################################################
	// ITEM PREFABS: PREFABS OF ALL OBJECTS THAT CAN BE FOUND IN THE GAME

	// ##### CONSUMABLES #####
	public GameObject objPotionHealth;
	public GameObject objPotionMana;
	public GameObject objPotionRejuv;
	public GameObject objHealthShard;
	public GameObject objMaxHealthShard;
	public GameObject objMaxMagicShard;
	public GameObject objMagicRegenShard;
	public GameObject objStrengthShard;
	public GameObject objToughnessShard;

	// ##### INVENTORY ITEMS #####
	public GameObject objWand;
	// ##### COLLECTIBLES #####
	public GameObject objPuzzlePiece;


	//######################################################################
	// ITEM VALUES: VALUES FOR ITEMS

	public int valPotionHealth;
	public int valPotionMana;
	public int valPotionRejuv;
	public int valHealthShard;
	public int valMaxHealthShard;
	public int valMaxMagicShard;
	public int valMagicRegenShard;
	public int valStrengthShard;
	public int valToughnessShard;

	// TODO: What do values for other items mean??


	//######################################################################
	// MEMBER VARIABLES

	// ##### LIST itemObjects #####
	private static List<GameObject> itemObjects;
	// ##### LIST itemValues #####
	private static List<int> itemValues;
	// ##### LIST itemNames #####
	private static List<string> itemNames;


	//######################################################################
	// FUNCTION Start: Load item values, objects, and names into lists
	private static bool awakeHasBeenCalled = false;
	void Awake()
	{
		if(!awakeHasBeenCalled)
		{
			itemObjects = new List<GameObject>((int)ItemType.COUNT);
			itemValues = new List<int>((int)ItemType.COUNT);
			itemNames = new List<string>((int)ItemType.COUNT);

			for(int i = 0; i < (int)ItemType.COUNT; ++i)
			{
				itemObjects.Add(null);
				itemValues.Add(0);
				itemNames.Add("");
			}

			// Consumables
			itemObjects[(int)ItemType.HealthShard]		= objHealthShard;
			itemObjects[(int)ItemType.PotionHealth]		= objPotionHealth;
			itemObjects[(int)ItemType.PotionMana] 		= objPotionMana;
			itemObjects[(int)ItemType.PotionRejuv] 		= objPotionRejuv;
			itemObjects[(int)ItemType.MaxHealthShard]	= objMaxHealthShard;
			itemObjects[(int)ItemType.MaxMagicShard]	= objMaxMagicShard;
			itemObjects[(int)ItemType.MagicRegenShard]	= objMagicRegenShard;
			itemObjects[(int)ItemType.StrengthShard]	= objStrengthShard;
			itemObjects[(int)ItemType.ToughnessShard]	= objToughnessShard;
			// Inventory Items
			itemObjects[(int)ItemType.Wand] 		= objWand;
			// Collectibles
			itemObjects[(int)ItemType.PuzzlePiece] 	= objPuzzlePiece;

			// Consumables
			itemValues[(int)ItemType.HealthShard]		= valHealthShard;
			itemValues[(int)ItemType.PotionHealth] 		= valPotionHealth;
			itemValues[(int)ItemType.PotionMana] 		= valPotionMana;
			itemValues[(int)ItemType.PotionRejuv] 		= valPotionRejuv;
			itemValues[(int)ItemType.MaxHealthShard]	= valMaxHealthShard;
			itemValues[(int)ItemType.MaxMagicShard]		= valMaxMagicShard;
			itemValues[(int)ItemType.MagicRegenShard]	= valMagicRegenShard;
			itemValues[(int)ItemType.StrengthShard]		= valStrengthShard;
			itemValues[(int)ItemType.ToughnessShard]	= valToughnessShard;


			// Consumables
			itemNames[(int)ItemType.HealthShard] 		= "Heart";
			itemNames[(int)ItemType.PotionHealth] 		= "PotionHealth";
			itemNames[(int)ItemType.PotionMana] 		= "PotionMana";
			itemNames[(int)ItemType.PotionRejuv] 		= "PotionRejuv";
			itemNames[(int)ItemType.MaxHealthShard] 	= "MaxHealthShard";
			itemNames[(int)ItemType.MaxMagicShard] 		= "MaxMagicShard";
			itemNames[(int)ItemType.MagicRegenShard] 	= "MagicRegenShard";
			itemNames[(int)ItemType.StrengthShard] 		= "StrengthShard";
			itemNames[(int)ItemType.ToughnessShard] 	= "ToughnessShard";
			// Inventory Items
			itemNames[(int)ItemType.Wand] 			= "Wand";
			// Collectibles
			itemNames[(int)ItemType.PuzzlePiece] 	= "PuzzlePiece";


			// TODO: Figure out why this is necessary for some Managers
			// Awake is called twice
			awakeHasBeenCalled = true;
		}
		else
		{
			Debug.Log("[ItemManager] Awake Called Twice!!!");
		}
	}
		

	//######################################################################
	// FUNCTION GetValue: returns the value of the specified type

	public static int GetValue(ConsumableType type)
	{
		return itemValues[(int)type];
	}
	public static int GetValue(PotionType type)
	{
		return itemValues[(int)type];
	}


	//######################################################################
	// FUNCTION GetObject: returns the object of the specified type

	public static GameObject GetObject(ConsumableType type)
	{
		return itemObjects[(int)type];
	}
	public static GameObject GetObject(PotionType type)
	{
		return itemObjects[(int)type];
	}
	public static GameObject GetObject(InventoryItemType type)
	{
		int index = (int)ConsumableType.COUNT + (int)type;
		return itemObjects[index];
	}
	public static GameObject GetObject(CollectibleType type)
	{
		int index = (int)ConsumableType.COUNT + (int)InventoryItemType.COUNT + (int)type;
		return itemObjects[index];
	}


	//######################################################################
	// FUNCTION GetName: returns the name of the specified type

	public static string GetName(ConsumableType type)
	{
		return itemNames[(int)type];
	}
	public static string GetName(PotionType type)
	{
		return itemNames[(int)type];
	}
	public static string GetName(InventoryItemType type)
	{
		int index = (int)ConsumableType.COUNT + (int)type;
		return itemNames[index];
	}
	public static string GetName(CollectibleType type)
	{
		int index = (int)ConsumableType.COUNT + (int)InventoryItemType.COUNT + (int)type;
		return itemNames[index];
	}

}
