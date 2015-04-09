using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


// ============================================================
// Scriptable Object Creation Functions
public static class SceneManagerDataUtility
{
	// ------------------------------------------------------------
	// Create Scriptable Object MenuItem
	[MenuItem("Assets/Create/SceneManagerData")]
	public static void CreateAsset ()
	{
		SceneManagerDataUtility.CreateNewObjectStates();
	}
	// ------------------------------------------------------------
	// Create Scriptable Object function
	public static void CreateNewObjectStates()
	{
		const int numData = 3;
		string path = "Assets/Data/SceneManagerData/";
		
		SceneManagerData[] sceneManagerData = new SceneManagerData[numData];
		string[] assetPathsAndNames = new string[numData];
		
		for(int i = 0; i < numData; ++i)
		{
			sceneManagerData[i] = ScriptableObject.CreateInstance<SceneManagerData>();
			sceneManagerData[i].objectStates = new List<ObjectState> ();
			sceneManagerData[i].objectStates.Add (new ObjectState("TEST", true));
			sceneManagerData[i].currentScene = Scenes.Frontend;
			sceneManagerData[i].transitionPointIndex = 0;
			assetPathsAndNames[i] = AssetDatabase.GenerateUniqueAssetPath (path + typeof(SceneManagerData).ToString() + "_" + i + ".asset");
			AssetDatabase.CreateAsset (sceneManagerData[i], assetPathsAndNames[i]);
		}
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		if(numData > 0)
		{
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = sceneManagerData[0];
		}
	}
}