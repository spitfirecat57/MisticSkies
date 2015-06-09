using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	private static int currentFileIndex = 0;
	private static string screenshotsFolderPath = Application.dataPath + "/Screenshots";
	private static int screenshotIndex = 1;
	private static bool isPaused = false;
	
	void Start()
	{
		screenshotIndex = PlayerPrefs.GetInt("ScreenshotIndex", 1);
	}
	
	void Update()
	{
		if(Input.GetKeyDown(InputManager.ScreenShotCode))
		{
			Debug.Log("[GameManager] Taking screenshot");
			TakeScreenshot();
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			Debug.Log("[GameManager] Saving Game");
			SaveGame();
		}

		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			PlayerManager.GetPlayerScript().IncreaseHealth(100);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			PlayerManager.GetPlayerScript().IncreaseFireMana(100);
			PlayerManager.GetPlayerScript().IncreaseWaterMana(100);
			PlayerManager.GetPlayerScript().IncreaseRockMana(100);
		}
	}
	
	public static void TakeScreenshot()
	{
		string shot = screenshotsFolderPath + string.Format ("/screenshot_{0:D04}.png", screenshotIndex++);
		TakeScreenshot(shot, 10);
	}
	public static void TakeScreenshot(int scale)
	{
		string shot = screenshotsFolderPath + string.Format ("/screenshot_{0:D04}.png", screenshotIndex++);
		TakeScreenshot(shot, scale);
	}
	public static void TakeScreenshot(string path)
	{
		TakeScreenshot(path, 1);
	}
	public static void TakeScreenshot(string path, int scale)
	{
		print ("[GameManager] TakingScreenshot("+path+", "+scale+")");
		Application.CaptureScreenshot(path, scale);
		PlayerPrefs.SetInt ("ScreenshotIndex", screenshotIndex);
		PlayerPrefs.Save();
	}
	
	
	
	public static void StartNewGameStatic()
	{
		// TODO: change to first level of the game, not testscene
		SceneManager.SetCurrentScene (Scenes.Level1);
		StateManager.ChangeState (GameStates.Gameplay);
	}
	public void StartNewGame()
	{
		// TODO: change to first level of the game, not testscene
		SceneManager.SetCurrentScene (Scenes.Level1);
		StateManager.ChangeState (GameStates.Gameplay);
	}

	public static void LoadGameStatic(int fileSlotIndex)
	{
		currentFileIndex = fileSlotIndex;
		PlayerManager.LoadGame(fileSlotIndex);
		InputManager.LoadGame (fileSlotIndex);
		SceneManager.LoadGame (fileSlotIndex);
		StateManager.ChangeState (GameStates.Gameplay);
	}
	public void LoadGame(int fileSlotIndex)
	{
		currentFileIndex = fileSlotIndex;
		PlayerManager.LoadGame(fileSlotIndex);
		InputManager.LoadGame (fileSlotIndex);
		SceneManager.LoadGame (fileSlotIndex);
		StateManager.ChangeState (GameStates.Gameplay);
	}
	
	public static void SaveGame()
	{
		PlayerManager.SaveGame(currentFileIndex);
		SceneManager.SaveGame(currentFileIndex);
		AssetDatabase.SaveAssets ();
	}
	
	public static void ExitGameStatic()
	{
		instance.ExitGame();
	}

	public void ExitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit;
		#endif
	}
	
	
	public static void PauseUnpauseGameplayStatic()
	{
		instance.PauseUnpauseGameplay();
	}
	
	public void PauseUnpauseGameplay()
	{
		if(isPaused)
		{
			// unpause
			UIManager.DeActivate(UICanvasTypes.Pause);
			Time.timeScale = 1.0f;
			InputManager.SetAcceptingInput(true);
			PlayerManager.GetCameraController().enabled = true;
			isPaused = false;
		}
		else
		{
			// pause
			UIManager.Activate(UICanvasTypes.Pause);
			Time.timeScale = 0.0f;
			InputManager.SetAcceptingInput(false);
			PlayerManager.GetCameraController().enabled = false;
			isPaused = true;
		}
	}

	public static void LoadCurrentGameStatic()
	{
		UIManager.ActivateDeathScreen();
		Time.timeScale = 0.01f;
		LoadGameStatic (currentFileIndex);
		SceneManager.LoadSceneDestructive (SceneManager.CurrentScene ());
	}


}











