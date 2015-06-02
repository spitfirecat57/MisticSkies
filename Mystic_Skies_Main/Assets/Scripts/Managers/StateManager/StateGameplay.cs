using UnityEngine;
using System.Collections;

public class StateGameplay : GameState
{
	public override void OnEnter()
	{
		print ("[StateGameplay] Loading gameplay scene: " + SceneManager.CurrentScene().ToString());
		InputManager.SetAcceptingInput (true);
		PlayerManager.SetPlayerAndCameraActive(true);
		SceneManager.LoadSceneDestructive (SceneManager.CurrentScene());
		UIManager.Activate (UICanvasTypes.HUD);
		BackgroundMusicManager.StartPlaying ();

#if UNITY_EDITOR
		Screen.showCursor = true;
#else
		Screen.showCursor = false;
#endif
		Screen.lockCursor = true;
	}
	
	public override void OnUpdate()
	{
		if(Input.GetKeyDown(InputManager.GetUIKeyCode(InputKeys.Exit)))
		{
			GameManager.PauseUnpauseGameplayStatic();
		}
		else if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.Inventory)))
		{
			//TODO: UIManager.Activate(Panels.Inventory);
		}

		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			GameManager.SaveGame();
		}
	}
	
	public override void OnExit()
	{
		UIManager.DeActivate (UICanvasTypes.HUD);
		Screen.showCursor = true;
		Screen.lockCursor = false;
		BackgroundMusicManager.StopPlaying();
	}
	
	
	
	
	
	
}
