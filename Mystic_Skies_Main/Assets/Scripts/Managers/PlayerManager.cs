using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	private static PlayerManager instance;
	
	public PlayerData playerDataFile0;
	public PlayerData playerDataFile1;
	public PlayerData playerDataFile2;
	private PlayerData[] playerData;
	
	public GameObject cameraPrefab;
	public GameObject playerPrefab;
	
	private static GameObject cameraObject = null;
	private static Camera cameraScript = null;
	private static CameraController cameraControllerScript = null;
	
	private static GameObject playerObject = null;
	private static Transform playerTransform = null;
	private static Player playerScript = null;
	private static PlayerController playerControllerScript = null;

	public static GameObject target = null;
	private static bool hasTarget = false;
	
	private static bool awakeHasBeenCalled = false;
	
	void Awake()
	{
		if(instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		if(!awakeHasBeenCalled)
		{
			playerData = new PlayerData[3];
			playerData[0] = playerDataFile0;
			playerData[1] = playerDataFile1;
			playerData[2] = playerDataFile2;
			
			
			print ("[PlayerManager] Awake()");
			GameObject playerObjectOther = GameObject.FindGameObjectWithTag("Player");
			if(playerObjectOther)
			{
				Debug.Log("[PlayerManager] Player already created. Grabbing it as reference.");
				
				playerObject = playerObjectOther;
				
				playerObject.name = "Player";
				DontDestroyOnLoad(playerObject);
				playerScript = playerObject.GetComponent<Player>();
				playerControllerScript = playerObject.GetComponent<PlayerController>();
				playerTransform = playerObject.transform;
				
				cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();
			}
			else if(playerPrefab && cameraPrefab)
			{
				Debug.Log("[PlayerManager] Instantiating player object.");
				playerObject = GameObject.Instantiate(playerPrefab, Vector3.up * 2.0f, Quaternion.identity) as GameObject;
				playerObject.name = "Player";
				DontDestroyOnLoad(playerObject);
				playerTransform = playerObject.transform;
				
				playerScript = playerObject.GetComponent<Player>();
				playerControllerScript = playerObject.GetComponent<PlayerController>();
				
				cameraObject = GameObject.Instantiate(cameraPrefab) as GameObject;
				cameraObject.name = "PlayerCamera";
				DontDestroyOnLoad(cameraObject);
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();

				playerObject.SetActive(true);
				cameraObject.SetActive(true);
			}
			else
			{
				Debug.Log("[PlayerManager] playerPrefab or cameraPrefab is null");
			}
			
			
			// TODO: Figure out why this is necessary for some Managers
			// Awake is called twice
			awakeHasBeenCalled = true;
		}
		else
		{
			Debug.Log("[PlayerManager] Awake Called Twice!!!");
		}
	}
	
	void Start()
	{
		if(playerObject == null)
		{
			Debug.Log("[PlayerManager] playerObject is null in Start(), reaquiring");
			playerObject = GameObject.FindGameObjectWithTag("Player");
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown (InputManager.GetKeyCode(InputKeys.CycleTarget)))
		{
			CycleTarget();
		}
		if (Input.GetKeyDown (InputManager.GetKeyCode(InputKeys.QuitTarget)))
		{
			target = null;
			hasTarget = false;
		}
		
		if(target)
		{
			hasTarget = true;
		}
		if(hasTarget && target == null)
		{
			CycleTarget();
		}
	}
	
	
	
	public static GameObject Target()
	{
		return target;
	}
	
	private void CycleTarget()
	{
		if(target)
		{
			target = EnemyManager.GetNextClosest(target, transform.position);
		}
		else
		{
			target = EnemyManager.GetClosest(transform.position);
		}
	}
	
	// ######################   Getters   ##################################
	
	public static GameObject GetPlayerObject()
	{
		return playerObject;
	}
	public static Transform GetPlayerTransform()
	{
		return playerTransform;
	}
	public static Vector3 GetPlayerPosition()
	{
		return playerTransform.position;
	}
	public static Quaternion GetPlayerRotation()
	{
		return playerTransform.rotation;
	}
	public static Vector3 GetPlayerForward()
	{
		return playerTransform.forward;
	}
	public static Vector3 GetPlayerRight()
	{
		return playerTransform.right;
	}
	public static Player GetPlayerScript()
	{
		return playerScript;
	}
	public static float GetPlayerHealth()
	{
		return playerScript.health;
	}
	public static float GetPlayerMaxHealth()
	{
		return playerScript.maxHealth;
	}
	public static float GetPlayerMana()
	{
		return playerScript.mana;
	}
	public static float  GetPlayerMaxMana()
	{
		return playerScript.maxMana;
	}
	public static PlayerController GetMovementScript()
	{
		return playerControllerScript;
	}
	public static GameObject GetCameraObject()
	{
		return cameraObject;
	}
	public static Transform GetCameraTransform()
	{
		return cameraObject.transform;
	}
	public static Camera GetCameraScript()
	{
		return cameraScript;
	}
	
	
	
	// ######################   Setters   ##################################
	
	public static void SetPlayerPositon(Vector3 pos)
	{
		playerTransform.position = pos;
	}
	public static void SetPlayerForward(Vector3 fwd)
	{
		playerTransform.forward = fwd;
	}
	public static void SetPlayerAndCameraActive(bool active)
	{
		playerObject.SetActive(active);
		cameraObject.SetActive(active);
	}
	
	
	
	
	
	// Save File Stuff
	public static float GetSaveFileHealth(int fileIndex)
	{
		return instance.playerData[fileIndex].health;
	}
	
	
	
	public static void LoadGame(int fileSlotindex)
	{
		PlayerManager.GetPlayerScript().LoadData(instance.playerData[fileSlotindex]);
	}
	
	public static void SaveGame(int fileSlotindex)
	{
		PlayerManager.GetPlayerScript().SaveData(instance.playerData[fileSlotindex]);
	}
	
	
}









