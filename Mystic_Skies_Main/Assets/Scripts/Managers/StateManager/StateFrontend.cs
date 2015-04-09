using UnityEngine;
using System.Collections;

public class StateFrontend : GameState
{
	public override void OnEnter()
	{
		SceneManager.LoadSceneDestructive(Scenes.Frontend);
		PlayerManager.SetPlayerAndCameraActive(false);
		InputManager.SetAcceptingInput (false);
		
		print("[StateFrontend] Click mouse to start game");
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnExit()
	{
	}
}
