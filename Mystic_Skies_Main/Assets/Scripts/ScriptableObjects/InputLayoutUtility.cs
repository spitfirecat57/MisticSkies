using UnityEngine;
using UnityEditor;
using System.IO;


public enum InputKeys
{
	Up,
	Down,
	Left,
	Right,
	Run,

	Interact,

	FireSpell,
	WaterSpell,
	RockSpell,
	MagicMode,

	CycleTarget,
	QuitTarget,

	HealthPotion,
	ManaPotion,
	RejuvPotion,

	Inventory,

	Exit,
	COUNT
}

public static class InputLayoutUtility
{
	// ------------------------------------------------------------
	// Create Scriptable Object MenuItem
	[MenuItem("Assets/Create/InputLayouts")]
	public static void CreateAsset ()
	{
		InputLayoutUtility.CreateDefaultInputLayout();
	}
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateDefaultInputLayout()
	{
		InputLayout asset1 = ScriptableObject.CreateInstance<InputLayout>();

		asset1.keys = new NameKeycode[(int)InputKeys.COUNT];
		for(int i = 0; i < asset1.keys.Length; ++i)
		{
			asset1.keys[i] = new NameKeycode(((InputKeys)i).ToString());
		}

		// default input layout 1
		string assetName1 = "Default_1";
		asset1.keys [(int)InputKeys.Up].key 				= KeyCode.W;
		asset1.keys [(int)InputKeys.Down].key 				= KeyCode.S;
		asset1.keys [(int)InputKeys.Left].key 				= KeyCode.A;
		asset1.keys [(int)InputKeys.Right].key 				= KeyCode.D;
		asset1.keys [(int)InputKeys.Run].key 				= KeyCode.LeftShift;

		asset1.keys [(int)InputKeys.Interact].key 			= KeyCode.F;

		asset1.keys [(int)InputKeys.FireSpell].key 			= KeyCode.Mouse0;
		asset1.keys [(int)InputKeys.WaterSpell].key 		= KeyCode.Mouse1;
		asset1.keys [(int)InputKeys.RockSpell].key 			= KeyCode.Mouse2;
		asset1.keys [(int)InputKeys.MagicMode].key 			= KeyCode.Tab;

		asset1.keys [(int)InputKeys.CycleTarget].key 		= KeyCode.E;
		asset1.keys [(int)InputKeys.QuitTarget].key 		= KeyCode.Q;

		asset1.keys [(int)InputKeys.HealthPotion].key 		= KeyCode.Alpha1;
		asset1.keys [(int)InputKeys.ManaPotion].key 		= KeyCode.Alpha2;
		asset1.keys [(int)InputKeys.RejuvPotion].key 		= KeyCode.Alpha3;

		asset1.keys [(int)InputKeys.Inventory].key 			= KeyCode.I;

		asset1.keys [(int)InputKeys.Exit].key 				= KeyCode.Escape;



//		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
//		if (path == "") 
//		{
//			path = "Assets";
//		} 
//		else if (Path.GetExtension (path) != "") 
//		{
//			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
//		}

		string path = "Assets/Data/Input/";
		
		string assetPathAndName1 = AssetDatabase.GenerateUniqueAssetPath (path + typeof(InputLayout).ToString() + "_" + assetName1 + ".asset");
		
		AssetDatabase.CreateAsset (asset1, assetPathAndName1);
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset1;
	}
}