using UnityEngine;
using System.Collections;

public enum GameStates
{
	Frontend,
	Gameplay,
	COUNT
}

public class StateManager : MonoBehaviour
{
	private static GameState[] mGameStates;
	private static GameState mCurrentGameState = null;
	
	private static GameStates currentState;
	private static GameStates nextState;
	
	public GameState stateFrontend;
	public GameState stateGameplay;
	
	
	void Start()
	{
		print ("[StateManager] Start()");
		mGameStates = new GameState[(int)GameStates.COUNT];
		mGameStates[(int)GameStates.Frontend] = stateFrontend;
		mGameStates[(int)GameStates.Gameplay] = stateGameplay;
		
		if(Application.loadedLevel != (int)Scenes.Frontend)
		{
			Debug.Log("[SceneManager] First Loaded level was not Frontend, Starting Gameplay");
			currentState = GameStates.Gameplay;
			nextState = GameStates.Gameplay;
			mCurrentGameState = mGameStates[(int)currentState];
			InputManager.SetAcceptingInput (true);
			PlayerManager.SetPlayerAndCameraActive(true);
			UIManager.Activate(UICanvasTypes.HUD);
			#if UNITY_EDITOR
			Screen.showCursor = true;
			#else
			Screen.showCursor = false;
			#endif
			Screen.lockCursor = true;
			BackgroundMusicManager.StartPlaying ();
		}
		else
		{
			currentState = GameStates.Frontend;
			nextState = GameStates.Frontend;
			mCurrentGameState = mGameStates[(int)GameStates.Frontend];
			PlayerManager.SetPlayerAndCameraActive(false);
			InputManager.SetAcceptingInput (false);
		}
	}
	
	void Update()
	{
		if(nextState != currentState)
		{
			mCurrentGameState.OnExit();
			mCurrentGameState = mGameStates[(int)nextState];
			mCurrentGameState.OnEnter();
			currentState = nextState;
		}
		mCurrentGameState.OnUpdate ();
	}
	
	public static void ChangeState(GameStates state)
	{
		print ("[StateManager] ChangeState("+state.ToString()+")");
		nextState = state;
	}
	
	
	
}
