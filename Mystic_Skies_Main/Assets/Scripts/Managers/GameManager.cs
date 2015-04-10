using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
	private static int currentFileIndex = 0;
	private static string screenshotsFolderPath = Application.dataPath + "/Screenshots";
	private static int screenshotIndex = 1;
	


	void Start()
	{
		screenshotIndex = PlayerPrefs.GetInt("ScreenshotIndex", 1);

		if(!gameObject.activeSelf || !gameObject.activeInHierarchy)
		{
			gameObject.SetActive(true);
		}
	}
	
	void Update()
	{
		if(Input.GetKeyDown(InputManager.ScreenShotCode))
		{
			Debug.Log("[GameManager] Taking screenshot");
			TakeScreenshot();
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			UIManager.NewDialogueBox("Dialogue Test");
		}
		else if(Input.GetKeyDown(KeyCode.Backspace))
		{
			UIManager.CloseDialogueBox();
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
	
	
	
	
	public static void StartNewGame(int fileSlotIndex)
	{
		currentFileIndex = fileSlotIndex;
		SceneManager.SetCurrentScene (Scenes.Level1);
		StateManager.ChangeState (GameStates.Gameplay);
	}
	
	public static void LoadGame(int fileSlotIndex)
	{
		currentFileIndex = fileSlotIndex;
		//TODO: PlayerManager.LoadGame(fileSlotindex);
		InputManager.LoadGame (fileSlotIndex);
		SceneManager.LoadGame(fileSlotIndex);
		StateManager.ChangeState (GameStates.Gameplay);
	}
	
	public static void SaveGame(int fileSlotIndex)
	{
		PlayerManager.SaveGame(currentFileIndex);
		SceneManager.SaveGame(currentFileIndex);
	}
	
	public static void ExitGame()
	{
		Application.Quit();
	}
	
	
	public static void PauseGameplay()
	{
		Time.timeScale = 0.0f;
		InputManager.SetAcceptingInput(false);
	}
	public static void UnPauseGameplay()
	{
		Time.timeScale = 1.0f;
		InputManager.SetAcceptingInput(true);
	}
	
	
}
