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
	private static SpellController playerSpellController = null;

	public static GameObject target = null;
	private static bool hasTarget = false;

	public GameObject currentTargetIndicator;
	private static GameObject CTI;
	
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
				print ("Got playerScript from reference");
				playerControllerScript = playerObject.GetComponent<PlayerController>();
				playerSpellController = playerObject.GetComponent<SpellController>();
				playerTransform = playerObject.transform;
				
				cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();

				DontDestroyOnLoad(playerObject);
				DontDestroyOnLoad(cameraObject);

				if(playerObject.transform.parent)
				{
					playerObject.transform.parent = null;
				}
				if(cameraObject.transform.parent)
				{
					cameraObject.transform.parent = null;
				}

				playerObject.SetActive(true);
				cameraObject.SetActive(true);
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
				playerSpellController = playerObject.GetComponent<SpellController>();
				
				cameraObject = GameObject.Instantiate(cameraPrefab) as GameObject;
				cameraObject.name = "PlayerCamera";
				DontDestroyOnLoad(cameraObject);
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();

				playerObject.SetActive(true);
				cameraObject.SetActive(true);

				DontDestroyOnLoad(playerObject);
				DontDestroyOnLoad(cameraObject);
			}
			else
			{
				Debug.Log("[PlayerManager] playerPrefab or cameraPrefab is null");
			}


			CTI = GameObject.Instantiate(currentTargetIndicator) as GameObject;
			CTI.transform.parent = gameObject.transform;
			CTI.SetActive(false);
			
			
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

	void OnLevelWasLoaded(int level)
	{
		print ("[PlayerManager] Lost Player reference");
		if(!playerObject)
		{
			GameObject playerObjectOther = GameObject.FindGameObjectWithTag("Player");
			if(playerObjectOther)
			{
				Debug.Log("[PlayerManager] Player already created. Grabbing it as reference.");
				
				playerObject = playerObjectOther;
				
				playerObject.name = "Player";
				DontDestroyOnLoad(playerObject);
				playerScript = playerObject.GetComponent<Player>();
				print ("Got playerScript from reference");
				playerControllerScript = playerObject.GetComponent<PlayerController>();
				playerSpellController = playerObject.GetComponent<SpellController>();
				playerTransform = playerObject.transform;
				
				cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();

				DontDestroyOnLoad(playerObject);
				DontDestroyOnLoad(cameraObject);
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
				playerSpellController = playerObject.GetComponent<SpellController>();
				
				cameraObject = GameObject.Instantiate(cameraPrefab) as GameObject;
				cameraObject.name = "PlayerCamera";
				DontDestroyOnLoad(cameraObject);
				cameraScript = cameraObject.GetComponent<Camera>();
				cameraControllerScript = cameraObject.GetComponent<CameraController>();
				
				playerObject.SetActive(true);
				cameraObject.SetActive(true);
			}
			DontDestroyOnLoad(playerObject);
			DontDestroyOnLoad(cameraObject);
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

		if(target)
		{
			CTI.transform.position = target.transform.position + (Vector3.up * 2.0f);
			CTI.transform.rotation = PlayerManager.GetPlayerRotation();
			CTI.SetActive(true);
		}
		else
		{
			CTI.SetActive(false);
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
			target = EnemyManager.GetNextClosest(target, PlayerManager.GetPlayerPosition(), playerScript.maxTargetingRange);
		}
		else
		{
			target = EnemyManager.GetClosest(PlayerManager.GetPlayerPosition(), playerScript.maxTargetingRange);
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
		if(playerScript)
		{
			return playerScript.health;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float GetPlayerMaxHealth()
	{
		if(playerScript)
		{
			return playerScript.maxHealth;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float GetPlayerFireMana()
	{
		if(playerScript)
		{
			return playerScript.fireMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float  GetPlayerMaxFireMana()
	{
		if(playerScript)
		{
			return playerScript.maxFireMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float GetPlayerWaterMana()
	{
		if(playerScript)
		{
			return playerScript.waterMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float  GetPlayerMaxWaterMana()
	{
		if(playerScript)
		{
			return playerScript.maxWaterMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float GetPlayerRockMana()
	{
		if(playerScript)
		{
			return playerScript.rockMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
	}
	public static float  GetPlayerMaxRockMana()
	{
		if(playerScript)
		{
			return playerScript.maxRockMana;
		}
		else
		{
			print ("playerScript is not set to an instance of a PlayerScript");
			return 0.0f;
		}
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
	public static CameraController GetCameraController()
	{
		return cameraControllerScript;
	}
	public static SpellController GetPlayerSpellController()
	{
		return playerSpellController;
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
		playerScript.LoadData(instance.playerData[fileSlotindex]);
	}
	
	public static void SaveGame(int fileSlotindex)
	{
		playerScript.SaveData(instance.playerData[fileSlotindex]);
	}
	
	
}









