using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


// ============================================================
// Scriptable Object Creation Functions
public static class PlayerDataUtility
{
	// ------------------------------------------------------------
	// Create Scriptable Object MenuItem
	[MenuItem("Assets/Create/PlayerData")]
	public static void CreateAsset ()
	{
		PlayerDataUtility.CreateNewObjectStates();
	}
	// ------------------------------------------------------------
	// Create Scriptable Object function
	public static void CreateNewObjectStates()
	{
		const int numData = 3;
		string path = "Assets/Data/PlayerData/";
		
		PlayerData[] playerManagerData = new PlayerData[numData];
		string[] assetPathsAndNames = new string[numData];
		
		for(int i = 0; i < numData; ++i)
		{
			playerManagerData[i] = ScriptableObject.CreateInstance<PlayerData>();
			
			// set initial data
			playerManagerData[i].health 		= 100;
			playerManagerData[i].maxHealth 		= 100;
			playerManagerData[i].fireMana 		= 100;
			playerManagerData[i].maxFireMana	= 100;
			playerManagerData[i].waterMana 		= 100;
			playerManagerData[i].maxWaterMana	= 100;
			playerManagerData[i].rockMana 		= 100;
			playerManagerData[i].maxRockMana	= 100;
			
			playerManagerData[i].potions = new List<int> ();
			for(int j = 0; j < (int)ItemManager.PotionType.COUNT; ++j)
			{
				playerManagerData[i].potions.Add(1);
			}
			
			playerManagerData[i].inventory = new List<int> ();
			for(int j = 0; j < (int)ItemManager.InventoryItemType.COUNT; ++j)
			{
				playerManagerData[i].inventory.Add(0);
			}
			
			playerManagerData[i].collectibles = new List<int> ();
			for(int j = 0; j < (int)ItemManager.CollectibleType.COUNT; ++j)
			{
				playerManagerData[i].collectibles.Add(0);
			}
			
			assetPathsAndNames[i] = AssetDatabase.GenerateUniqueAssetPath (path + typeof(PlayerData).ToString() + "_" + i + ".asset");
			AssetDatabase.CreateAsset (playerManagerData[i], assetPathsAndNames[i]);
		}
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		if(numData > 0)
		{
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = playerManagerData[0];
		}
	}
}