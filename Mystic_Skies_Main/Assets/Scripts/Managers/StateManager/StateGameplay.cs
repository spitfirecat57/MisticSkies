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
	}
	
	public override void OnUpdate()
	{
		if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.Exit)))
		{
			//Time.timeScale = 0.0f;
			
			GameManager.PauseGameplay();
			UIManager.Activate(UICanvasTypes.Pause);
			
			//currentInputFunc = PauseInput;
		}
		else if(Input.GetKeyDown(InputManager.GetKeyCode(InputKeys.Inventory)))
		{
			//TODO: UIManager.Activate(Panels.Inventory);
		}
	}
	
	public override void OnExit()
	{
		//		if(UIManager.IsAnyPanelActive())
		//		{
		//			UIManager.CloseActivePanel();
		//		}
		UIManager.DeActivate (UICanvasTypes.HUD);
	}
	
	
	
	
	
	
}
