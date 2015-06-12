using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float distFromPlayer;
	public float zoomSpeed;
	public float zoomFalloffMultiplier;
	public float maxDistFromPlayer;
	public float minDistFromPlayer;
	public float lookAtHeightOffset;
	public float rotYSpeed;
	public float rotXSpeed;
	public float xMinAngle;
	public float xMaxAngle;
	public Vector3 focalPoint = (Vector3.up * 2.0f) + (Vector3.back * 2.0f);
	public float targetingHeightMultiplier = 1.0f;
	
	private float rotY;
	private float rotX;
	
	private Transform playerTransform;

	
	void Start()
	{
		rotX = 45.0f;
		rotY = -90.0f;
		
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if(playerObject)
		{
			playerTransform = playerObject.transform;
		}
		else
		{
			print("[CameraController] Could not find Player");
		}
		if(zoomFalloffMultiplier == 0.0f)
		{
			zoomFalloffMultiplier = 1.0f;
		}
	}
	
	void Update()
	{
		GameObject target = PlayerManager.Target();
		
		playerTransform = PlayerManager.GetPlayerTransform ();
		
		// scroll to zoom regular camera
		// TODO: scroll to move action cam??
		float deltaScroll = Input.GetAxis ("Mouse ScrollWheel");
		
		distFromPlayer -= deltaScroll * zoomSpeed * (distFromPlayer / zoomFalloffMultiplier);
		if(distFromPlayer > maxDistFromPlayer)
		{
			distFromPlayer = maxDistFromPlayer;
		}
		if(distFromPlayer < minDistFromPlayer)
		{
			distFromPlayer = minDistFromPlayer;
		}
		
		// no target, regular camera movement
		if(target == null)
		{
			//get input
			rotY -= Input.GetAxis ("Mouse X") * rotYSpeed;
			rotX -= Input.GetAxis ("Mouse Y") * rotXSpeed;
			
			rotX = Mathf.Clamp (rotX, xMinAngle, xMaxAngle);
			
			Vector3 newCameraPos = Vector3.zero;
			
			newCameraPos.x = Mathf.Cos (Mathf.Deg2Rad * rotY);
			newCameraPos.z = Mathf.Sin (Mathf.Deg2Rad * rotY);
			newCameraPos.y = Mathf.Sin (Mathf.Deg2Rad * rotX);
			
			newCameraPos *= distFromPlayer;		
			
			transform.position = playerTransform.position + newCameraPos;
			transform.LookAt (playerTransform.position + (Vector3.up * lookAtHeightOffset));

			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistFromPlayer))
			{
				if(hit.collider.CompareTag("Floor"))
				{
					//print ("[Camera] Behind the terrain, correcting position");
					transform.position = hit.point + (Vector3.up * 0.2f);
					transform.LookAt (playerTransform.position + (Vector3.up * lookAtHeightOffset));
				}
			}

		}
		// target, action cam
		else
		{
			Vector3 targetPos = target.transform.position + (Vector3.up * targetingHeightMultiplier);
			Vector3 newfocalPoint = playerTransform.position + (Vector3.up * lookAtHeightOffset) + (playerTransform.up * focalPoint.y);
			Vector3 fromTargetToFocalPoint = (newfocalPoint - targetPos).normalized;
			Vector3 camPos = newfocalPoint + (fromTargetToFocalPoint * focalPoint.z) + (playerTransform.right * focalPoint.x);
			transform.position = camPos;
			transform.LookAt (targetPos);
		}
	}
	
	
}
