using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Scenes
{
	Frontend,
	Level1,
	test_1,
	test_2,
	JustForJesse,
	COUNT
}

[RequireComponent(typeof(ReadSceneNames))]

public class SceneManager : MonoBehaviour
{	
	private static SceneManager instance;
	
	private static string[] sceneNames;
	
	private static List<ObjectState> objectStates;
	private static Scenes currentScene;
	private static int transitionPointIndex = 0;
	
	public SceneManagerData sceneManagerDataFile0;
	public SceneManagerData sceneManagerDataFile1;
	public SceneManagerData sceneManagerDataFile2;
	private SceneManagerData[] sceneManagerData;
	
	
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
			//print ("[SceneManager] Awake()");
			Debug.Log("["+this.GetType().ToString()+"]::GetSceneIndex");
			
			ReadSceneNames readSceneNames = GetComponent<ReadSceneNames>();
			
			if(readSceneNames)
			{
				sceneNames = readSceneNames.scenes;
				if(sceneNames.Length != (int)Scenes.COUNT)
				{
					Debug.Log("[SceneManager] Number of scenes read does not match number of scenes in 'Scenes' enum\n" +
					          "               Click here to read troubleshooting options\n" +
					          "               Check 'File/Build Settings' for scenes included in build\n" +
					          "               Check 'Scenes' enum in 'SceneManager' script\n" +
					          "               Update 'ReadSceneNames' component in '" + name + "' gameobject");
				}
			}
			else
			{
				Debug.Log("[SceneManager] readSceneNames is null");
			}
			
			if(Application.loadedLevelName != sceneNames[(int)Scenes.Frontend])
			{
				Debug.Log("[SceneManager] Loaded level was not Frontend Screen. Loaded level was " + (Scenes)Application.loadedLevel);
				currentScene = (Scenes)Application.loadedLevel;
			}
			else
			{
				currentScene = Scenes.Frontend;
			}
			
			
			if(sceneManagerDataFile0 == null)
			{
				Debug.Log("[SceneManager] sceneManagerDataFile0 is NULL!!!");
			}
			if(sceneManagerDataFile1 == null)
			{
				Debug.Log("[SceneManager] sceneManagerDataFile1 is NULL!!!");
			}
			if(sceneManagerDataFile2 == null)
			{
				Debug.Log("[SceneManager] sceneManagerDataFile2 is NULL!!!");
			}
			
			sceneManagerData = new SceneManagerData[3];
			sceneManagerData[0] = sceneManagerDataFile0;
			sceneManagerData[1] = sceneManagerDataFile1;
			sceneManagerData[2] = sceneManagerDataFile2;
			
			// initialize current state info
			objectStates = new List<ObjectState>();
			transitionPointIndex = 0;
			
			// TODO: Figure out why this is necessary for some Managers
			// Awake is called twice
			awakeHasBeenCalled = true;
		}
		else
		{
			Debug.Log("[SceneManager] Awake Called Twice!!!");
		}
	}
	
	public static Scenes CurrentScene()
	{
		return currentScene;
	}
	public static void SetCurrentScene(Scenes scene)
	{
		currentScene = scene;
	}
	
	public static void SetCurrentScene(string sceneName)
	{
		currentScene = (Scenes)GetSceneIndex(sceneName);
	}
	
	public static string GetSceneName(Scenes scene)
	{
		return sceneNames[(int)scene];
	}
	
	public static int GetSceneIndex(string sceneName)
	{
		for(int i = 0; i < sceneName.Length; ++i)
		{
			if(sceneNames[i] == sceneName)
			{
				return i;
			}
		}
		Debug.Log("["+instance.GetType().ToString()+"]::GetSceneIndex - '" + sceneName + "' is an invalid scene name!!!");
		return (int)Scenes.COUNT;
	}
	
	public static void LoadSceneDestructive(Scenes scene)
	{
		// TODO: 
		//UIManager.Activate (UICanvasTypes.Loading);
		print ("[SceneManager] LoadSceneDestructive("+scene.ToString()+")");
		currentScene = scene;
		Application.LoadLevel((int)scene);
	}
	
	private static void LoadSceneAdditive(Scenes scene)
	{
		print ("[SceneManager] LoadSceneAdditive("+scene.ToString()+")");
		Application.LoadLevelAdditive((int)scene);
	}
	
	#if UNITY_PRO_LICENSE
	public static void LoadSceneDestructiveAsync(Scenes scene)
	{
		print ("[SceneManager] Loading Scene '" + scene.ToString() + "' Destructively");
		Application.LoadLevelAsync(sceneNames[(int)scene]);
		currentScene = scene;
	}
	
	public static void LoadSceneAdditiveAsync(Scenes scene)
	{
		print ("[SceneManager] Loading Scene '" + scene.ToString() + "' Additively");
		Application.LoadLevelAdditiveAsync(sceneNames[(int)scene]);
	}
	#endif
	
	public static void SetTransitionPointIndex(int index)
	{
		transitionPointIndex = index;
	}
	public static int GetTransitionPointIndex()
	{
		return transitionPointIndex;
	}
	
	public static bool GetWasInteracted(string objName)
	{		
		ObjectState state = objectStates.Find(obj => obj.name == objName);
		if(state.IsValid())
		{
			return state.state;
		}
		return false;
	}
	
	public static void SetWasInteracted(string objName, bool interacted)
	{
		ObjectState objState = objectStates.Find(obj => obj.name == objName);
		
		if (objState.IsValid())
		{
			objState = new ObjectState(objName, interacted);
		}
		else
		{
			objectStates.Add(new ObjectState(objName, interacted));
		}
	}
	
	
	
	public static Scenes GetSaveFileScene(int fileIndex)
	{
		print ("sceneManagerData[" + fileIndex + "] = " + ((SceneManager)instance).sceneManagerData[fileIndex].currentScene);
		return instance.sceneManagerData [fileIndex].currentScene;
	}
	
	
	
	public static void LoadGame(int fileSlotindex)
	{
		currentScene 			= instance.sceneManagerData [fileSlotindex].currentScene;
		transitionPointIndex 	= instance.sceneManagerData [fileSlotindex].transitionPointIndex;
		objectStates 			= instance.sceneManagerData [fileSlotindex].objectStates;
	}
	
	public static void SaveGame(int fileSlotindex)
	{
		print ("[SceneManager] Saving: scene = " + currentScene + "TPI = " + transitionPointIndex);
		instance.sceneManagerData [fileSlotindex].currentScene 			= currentScene;
		instance.sceneManagerData [fileSlotindex].transitionPointIndex 	= transitionPointIndex;
		instance.sceneManagerData [fileSlotindex].objectStates 			= objectStates;
	}
	
	
	
	
}








