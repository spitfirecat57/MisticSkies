using UnityEngine;
using System.Collections;

public class StopStartTime : MonoBehaviour
{
	public void StartTime()
	{
		print ("[StopStartTime] Time.TimeScale = 1.0f");
		Time.timeScale = 1.0f;
	}

	public void StopTime()
	{
		print ("[StopStartTime] Time.TimeScale = 0.0f");
		Time.timeScale = 0.0f;
	}
}
