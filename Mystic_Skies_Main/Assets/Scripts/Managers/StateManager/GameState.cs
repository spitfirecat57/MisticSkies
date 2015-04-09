using UnityEngine;
using System.Collections;

public abstract class GameState : MonoBehaviour
{
	public abstract void OnEnter();

	public abstract void OnUpdate();

	public abstract void OnExit();
}
