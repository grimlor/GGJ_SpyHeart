using UnityEngine;
using System.Collections;
using Utilities;

public class EntryScene : MonoBehaviour {
	
	private void Start()
	{
		// API Login
		string param1 = "10.6";
		string param2 = "30.5";
		
		// TODO: Pass in two callbacks
		API.Instance.Call("Register", onCallCompleted, param1, param2 );
	}
	
	// TODO: Change to two callbacks: OnRegistered Callback & OnHeartbeat
	public void onCallCompleted(APIEvent e)
	{
		Debug.Log("Got callback");

		RegisterResponse response = Parsing.json_decode<RegisterResponse>(e.Response);
		
		Debug.Log ("UserId: " + response.RegisterResult.UserId);
		Debug.Log ("Latitude: " + response.RegisterResult.Latitude);
		Debug.Log ("Longitude: " + response.RegisterResult.Longitude);
		
		Globals.UserID = response.RegisterResult.UserId;
		
		Application.LoadLevel("GameScene");
	}
}
