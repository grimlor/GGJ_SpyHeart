using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	void TargetClicked() 
	{
		GameManager.Target = true;
		Application.LoadLevel("Game");
		Debug.Log("Target");
	}
	
	void TrackerClicked()
	{
		Application.LoadLevel("Game");
		Debug.Log("Tracker");
	}
	
}
