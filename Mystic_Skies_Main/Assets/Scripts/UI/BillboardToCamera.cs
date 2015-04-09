using UnityEngine;
using System.Collections;

public class BillboardToCamera : MonoBehaviour
{
	private Transform cameraTransform;
	void Start()
	{
		cameraTransform = PlayerManager.GetCameraObject().transform;
	}

	void Update()
	{
		Vector3 toCamera = cameraTransform.position - transform.position;
		transform.forward = toCamera.normalized;
	}
}
