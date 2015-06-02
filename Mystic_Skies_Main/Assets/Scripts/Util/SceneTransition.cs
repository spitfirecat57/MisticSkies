using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
	private const float kSpawnOffset = 2.0f;
	public int transitionPointIndex;
	public bool loadSceneOnCollision = true;
	public Scenes sceneToload;

	void Start()
	{
		if(SceneManager.GetTransitionPointIndex() == this.transitionPointIndex)
		{
			PlayerManager.SetPlayerPositon (transform.position + (transform.forward * kSpawnOffset) + (transform.up * kSpawnOffset));
			PlayerManager.SetPlayerForward (transform.forward);
		}
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		if(mr)
		{
			mr.enabled = false;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.CompareTag("Player"))
		{
			SceneManager.SetTransitionPointIndex(this.transitionPointIndex);
			if(loadSceneOnCollision)
			{
				SceneManager.LoadSceneDestructive(sceneToload);
			}
			else
			{
				SceneManager.SetCurrentScene(sceneToload);
			}
		}
	}
}