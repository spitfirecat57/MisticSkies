using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ============================================================
// scriptable object of player data
[System.Serializable]
public class PlayerData : ScriptableObject
{
	public int health;
	public int maxHealth;
	public int mana;
	public int maxMana;
	
	public List<int> potions;
	public List<int> inventory;
	public List<int> collectibles;
}

