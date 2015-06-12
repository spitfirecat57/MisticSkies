using UnityEngine;
using System.Collections;

public class SpellController : MonoBehaviour
{
	private float attackTimer = 0.0f;
	public  float LQkDelayTime = 0.5f;
	public  float HQDelayTime = 1.0f;
	public int HQAttackManaCost = 50;
	public int fireAttackManaCost = 10;
	public int waterAttackManaCost = 10;
	public int rockAttackManaCost = 10;

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


		Player p = PlayerManager.GetPlayerScript ();

		// switch between low and high magic
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.MagicMode)))
		{
			HQMagic = !HQMagic;
		}


		if(attackTimer < 0.0f)
		{			
			// left click == fire spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.FireSpell)))
			{
				if(HQMagic && p.fireMana > HQAttackManaCost)
				{
					mSpells[(int)Spells.Fire2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseFireMana(-HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else if( p.fireMana > fireAttackManaCost)
				{
					mSpells[(int)Spells.Fire1].Cast ();
					PlayerManager.GetPlayerScript().IncreaseFireMana(-fireAttackManaCost);
					attackTimer = LQkDelayTime;
				}
			}
			
			// right click == water spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.WaterSpell)))
			{
				if(HQMagic && p.waterMana > HQAttackManaCost)
				{
					mSpells[(int)Spells.Water2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseWaterMana(-HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else if( p.waterMana > waterAttackManaCost)
				{
					mSpells[(int)Spells.Water1].Cast ();
					PlayerManager.GetPlayerScript().IncreaseWaterMana(-waterAttackManaCost);
					attackTimer = LQkDelayTime;
				}
			}
			
			// middle click == rock spells
			if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.RockSpell)))
			{
				if(HQMagic && p.rockMana > HQAttackManaCost)
				{
					mSpells[(int)Spells.Rock2].Cast ();
					PlayerManager.GetPlayerScript().IncreaseRockMana(-HQAttackManaCost);
					attackTimer = HQDelayTime;
				}
				else if(p.rockMana > rockAttackManaCost)
				{
					mSpells[(int)Spells.Rock1].Cast ();
					PlayerManager.GetPlayerScript().IncreaseRockMana(-rockAttackManaCost);
					attackTimer = LQkDelayTime;
				}
			}
		}
	}


	public void IncreaseStrength(float val)
	{
		foreach(Spell spell in mSpells)
		{
			spell.damage *= 1.0f + (val / 100.0f);
		}
	}






}






















