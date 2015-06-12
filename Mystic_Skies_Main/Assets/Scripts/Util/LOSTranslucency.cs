using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LOSTranslucency : MonoBehaviour
{
	List<MeshRenderer> toFadeOut;
	List<MeshRenderer> toFadeIn;
	//private Transform playerTransform;

	void Start()
	{
		toFadeOut = new List<MeshRenderer>();
		toFadeIn = new List<MeshRenderer>();
		StartCoroutine("CastRay");
		StartCoroutine("FadeOut");
		//StartCoroutine("FadeIn");
	}

//	void OnEnable()
//	{
//		StartCoroutine("CastRay");
//		StartCoroutine("FadeOut");
//	}

	IEnumerator CastRay()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			Vector3 toPlayer = PlayerManager.GetPlayerTransform().position - transform.position;
			Ray ray = new Ray(transform.position, toPlayer.normalized);
			RaycastHit[] hitInfos = Physics.RaycastAll(ray, toPlayer.magnitude - 1.0f);
			MeshRenderer[] mrenderer = null;

			foreach(RaycastHit hit in hitInfos)
			{
				if(!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("InteractionBox"))
				{
					mrenderer = hit.collider.GetComponentsInChildren<MeshRenderer>();
					foreach(MeshRenderer r in mrenderer)
					{
						if(r)
						{
							if(toFadeIn.Contains(r))
							{
								toFadeIn.Remove(r);
							}
							if(!toFadeOut.Contains(r))
							{
								toFadeOut.Add(r);
							}
						}
					}
				}
			}

			yield return null;
		}
	}

	IEnumerator FadeOut()
	{
		while(true)
		{
			// fade out
			foreach(MeshRenderer r in toFadeOut)
			{
				if(r)
				{
					Color c = r.material.color;
					if(c.a > 0.1f)
					{
						c.a -= 0.05f;
					}
					r.material.color = c;
				}
			}
			toFadeOut.RemoveAll(obj => obj == null);
			// check for fade in additions
			foreach(MeshRenderer mr in toFadeOut.FindAll(obj => obj.material.color.a < 0.15f))
			{
				toFadeIn.Add(mr);
			}
			toFadeOut.RemoveAll(obj => obj.material.color.a < 0.15f);

			// fade in
			foreach(MeshRenderer r in toFadeIn)
			{
				if(r)
				{
					Color c = r.material.color;
					if(c.a < 1.0f)
					{
						c.a += 0.05f;
					}
					r.material.color = c;
				}
			}
			toFadeIn.RemoveAll(obj => (obj == null) || (obj.material.color.a == 1.0f));
			
			yield return new WaitForEndOfFrame();
		}
	}

//	IEnumerator FadeIn()
//	{
//		while(true)
//		{
//			foreach(MeshRenderer r in toFadeIn)
//			{
//				Color c = r.material.color;
//				if(c.a < 1.0f)
//				{
//					c.a += 0.05f;
//				}
//				r.material.color = c;
//			}
//			toFadeIn.RemoveAll(obj => obj.material.color.a == 1.0f);
//
//			yield return null;
//		}
//	}



}
