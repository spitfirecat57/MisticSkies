using UnityEngine;
using System.Collections;

public class FlameTowerFlame : MonoBehaviour
{
	public float damage;
	public float rotSpeed;
	public float expansionSpeed;
	public Vector3 originPos;

	void Update()
	{
		Vector3 startpos = new Vector3 (originPos.x, transform.position.y, originPos.z);
		transform.RotateAround (startpos, Vector3.up, 1.5f);

		transform.position += (transform.position - startpos).normalized * Time.deltaTime * 4.0f;
		transform.position += Vector3.up * (PlayerManager.GetPlayerPosition () - transform.position).y * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			PlayerManager.GetPlayerScript().TakeDamage(damage);
			PlayerManager.GetPlayerScript().KnockBack((PlayerManager.GetPlayerPosition() - transform.position).normalized);
			Destroy(gameObject);
		}
	}
}
