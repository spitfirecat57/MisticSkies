using UnityEngine;
using System.Collections;

public class BillboardToCameraReversed : MonoBehaviour
{
	private Transform cameraTransform;
	void Start()
	{
		cameraTransform = PlayerManager.GetCameraObject().transform;
	}

	void Update()
	{
		Vector3 fromCamera = cameraTransform.position - transform.position;
		transform.forward = fromCamera.normalized;
	}
}
