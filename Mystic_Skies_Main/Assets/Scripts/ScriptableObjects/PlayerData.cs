﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ============================================================
// scriptable object of player data
[System.Serializable]
public class PlayerData : ScriptableObject
{
	public float health;
	public float maxHealth;
	public float fireMana;
	public float maxFireMana;
	public float waterMana;
	public float maxWaterMana;
	public float rockMana;
	public float maxRockMana;
	
	public List<int> potions;
	public List<int> inventory;
	public List<int> collectibles;
}

