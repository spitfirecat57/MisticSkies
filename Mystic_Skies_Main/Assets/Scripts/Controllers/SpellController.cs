using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
	private float attackTimer = 0.0f;
	public  float LQkDelayTime = 0.5f;
	public  float HQDelayTime = 1.0f;
	public int HQAttackManaCost = -50;

	public Transform spellSpawnTransform;
	
	public Spell spellFire1;
	public Spell spellFire2;
	public Spell spellWater1;
	public Spell spellWater2;
	public Spell spellRock1;
	public Spell spellRock2;
	
	public enum Spells
	{
		Fire1,
		Fire2,
		Water1,
		Water2,
		Rock1,
		Rock2,
		COUNT
	}
	private Spell[] mSpells;
	
	private bool HQMagic;
	
	void Start()
	{
		mSpells = new Spell[(int)Spells.COUNT];
		mSpells[(int)Spells.Fire1] 	= spellFire1;
		mSpells[(int)Spells.Fire2] 	= spellFire2;
		mSpells[(int)Spells.Water1] = spellWater1;
		mSpells[(int)Spells.Water2] = spellWater2;
		mSpells[(int)Spells.Rock1] 	= spellRock1;
		mSpells[(int)Spells.Rock2] 	= spellRock2;
		foreach (Spell s in mSpells)
		{
			s.SetOwner(this);
		}
	}
	
	void Update()
	{
		attackTimer -= Time.deltaTime;


		// switch between low and high magic
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.MagicMode)))
		{
			HQMagic = !HQMagic;
		}

		if(PlayerManager.GetPlayerMana() < 50)
		{
			HQMagic = false;
		}



		if(attackTimer < 0.0f)
		{			
			// left click == fire spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.FireSpell)))
			{
				if(HQMagic)
				{
					mSpells[(int)Spells.Fire2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseMana(HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else
				{
					mSpells[(int)Spells.Fire1].Cast ();
					attackTimer = LQkDelayTime;
				}
			}
			
			// right click == water spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.WaterSpell)))
			{
				if(HQMagic)
				{
					mSpells[(int)Spells.Water2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseMana(HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else
				{
					mSpells[(int)Spells.Water1].Cast ();
					attackTimer = LQkDelayTime;
				}
			}
			
			// middle click == rock spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.RockSpell)))
			{
				if(HQMagic)
				{
					mSpells[(int)Spells.Rock2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseMana(HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else
				{
					mSpells[(int)Spells.Rock1].Cast ();
					attackTimer = LQkDelayTime;
				}
			}
		}
	}
}



