using UnityEngine;
using System.Collections;

public enum SpellType
{
	Fire,
	Water,
	Rock
};

public abstract class Spell : MonoBehaviour
{
	protected SpellController owner = null;

	public void SetOwner(SpellController sc)
	{
		owner = sc;
	}
	public SpellController GetOwner()
	{
		return owner;
	}

	abstract public void Cast();


	private static float kAdvantage = 2.0f;
	private static float kNeutral = 1.0f;
	private static float kDisadvantage = 0.5f;
	
	static public float GetDamageMultiplier(SpellType attackType, SpellType defendType)
	{
		switch(attackType)
		{
		case SpellType.Fire:
		{
			switch(defendType)
			{
			case SpellType.Fire:
			{
				return kNeutral;
			}
			case SpellType.Rock:
			{
				return kAdvantage;
			}
			case SpellType.Water:
			{
				return kDisadvantage;
			}
			default:
			{
				return 0.0f;
			}
			}
		}
		case SpellType.Rock:
		{
			switch(defendType)
			{
			case SpellType.Fire:
			{
				return kDisadvantage;
			}
			case SpellType.Rock:
			{
				return kNeutral;
			}
			case SpellType.Water:
			{
				return kAdvantage;
			}
			default:
			{
				return 0.0f;
			}
			}
		}
		case SpellType.Water:
		{
			switch(defendType)
			{
			case SpellType.Fire:
			{
				return kAdvantage;
			}
			case SpellType.Rock:
			{
				return kDisadvantage;
			}
			case SpellType.Water:
			{
				return kNeutral;
			}
			default:
			{
				return 0.0f;
			}
			}
		}
		default:
		{
			return 0.0f;
		}
		}		
	}
}
