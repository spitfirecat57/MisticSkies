using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class enemyAttackBox : MonoBehaviour
{
	public float damage;
	private BoxCollider boxCollider;

	void Start()
	{
		boxCollider = GetComponent<BoxCollider> ();
		boxCollider.isTrigger = true;
		Destroy (gameObject, 0.2f);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Player player = other.GetComponent<Player>();
			if(player)
			{
				player.TakeDamage(damage);
			}
			Destroy(gameObject);
		}
	}
}
