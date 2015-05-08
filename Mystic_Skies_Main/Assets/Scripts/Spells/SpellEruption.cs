using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellEruption : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public SpellType type;
		public float minDistForward = 5.0f;
		public float maxDistForward = 20.0f;
		public float maxDistSide = 5.0f;
		public float maxDistUpDown = 10.0f;
		
		public float duration = 1.0f;
		
		public int numExplosions = 5;
		public float explosionDamage = 5.0f;
		public float explosionRadius = 5.0f;
	}
	public Loadout loadout;

	public GameObject explosionObject;



	private float mTimer = 0.0f;
	private bool mIsCasted = false;
	private int mExplosionIndex = 0;
	private Vector3 playerPosition;
	private Vector3 playerForward;
	private Vector3 playerRight;



	override public void Cast()
	{
		mIsCasted = true;

		mExplosionIndex = 0;

		playerPosition = PlayerManager.GetPlayerPosition();
		playerForward = PlayerManager.GetPlayerForward();
		playerRight = PlayerManager.GetPlayerRight();
	}

	void Update()
	{
		if(mIsCasted)
		{
			mTimer += Time.deltaTime;

			//if(mTimer > mExplosionTimes[mExplosionIndex])
			if(mTimer > (mExplosionIndex * (loadout.duration / loadout.numExplosions)))
			{
				Debug.Log("[SpellEruption] Explosion " + (mExplosionIndex + 1));
				Vector3 sideOffset = playerRight * Random.Range(-loadout.maxDistSide, loadout.maxDistSide);
				float forwardIncr = (loadout.maxDistForward - loadout.minDistForward) / (loadout.numExplosions - 1);
				Vector3 forwardOffset = playerForward * ((mExplosionIndex + 1) * forwardIncr);
				Vector3 explosionPoint = playerPosition + sideOffset + forwardOffset + (Vector3.up * loadout.maxDistUpDown * 0.5f);


				// get y value for explosion point from terrain
				RaycastHit hitInfo;
				if(Physics.Raycast(explosionPoint, Vector3.down, out hitInfo, loadout.maxDistUpDown))//, LayerMask.NameToLayer("Terrain")))
				{
					GameObject.Instantiate(explosionObject, hitInfo.point, Quaternion.identity);

					Collider[] cols = Physics.OverlapSphere (hitInfo.point, loadout.explosionRadius); //, LayerMask.NameToLayer("Enemy"));
					foreach(Collider col in cols)
					{
						if(col.CompareTag("Enemy"))
						{
							Enemy enemy = col.GetComponent<Enemy>();
							if(enemy)
							{
								//Debug.Log("[SpellEruption] Explosion hit an enemy");
								enemy.TakeDamage(loadout.type, loadout.explosionDamage);
							}
						}
						else if(col.CompareTag("FireBoss"))
						{
							FireBoss enemy = col.GetComponent<FireBoss>();
							if(enemy)
							{
								enemy.TakeDamage(loadout.type, loadout.explosionDamage);
							}
						}

					}
				}


				++mExplosionIndex;
				if(mExplosionIndex == loadout.numExplosions)
				{
					mIsCasted = false;
					mTimer = 0.0f;
					mExplosionIndex = 0;
				}
			}
		}
	}
}
