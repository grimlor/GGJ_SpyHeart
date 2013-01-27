using UnityEngine;
using System.Collections;
using Utilities;
using System;

public class GameScene : MonoBehaviour
{
	void Awake()
	{
		Gui.CommonLabel = GameObject.Find("Common Label").GetComponent<UILabel>();
		Gui.CommonLabel.text = "";
		Gui.ScoreLabel = GameObject.Find("Score Label").GetComponent<UILabel>();
		Gui.ScoreLabel.text = "";
		Gui.TimerLabel = GameObject.Find("Timer Label").GetComponent<UILabel>();
		Gui.TimerLabel.text = "";
	}
	void Start ()
	{
		//Locator.LocationResult result = Locator.Instance.InitializeLocationServices();
		//if(!result.Success)
		//	Debug.Log(result.Message);
		
		InvokeRepeating("OnHeartBeat", 2f, 5f);
	}
	
	void Update ()
	{
	
	}
	
	private void OnHeartBeat()
	{
		try
		{
			API.Instance.Call("CheckIn", onGotHeartBeat, Globals.UserID, 
				Locator.Instance.GetLocation().latitude,
				Locator.Instance.GetLocation().longitude,
				Globals.CurGameState.ToString("D") );
		}
		catch(Exception)
		{
			Debug.Log ("Waiting Patiently");
		}
	}
	
	public void onGotHeartBeat(APIEvent e)
	{
		Debug.Log (e.Response);
		
		CheckInResponse response = Parsing.json_decode<CheckInResponse>(e.Response);
		
		Globals.CurGameState = (Globals.GameState)response.CheckInResult.CurGameState;
		
		if(response.CheckInResult.TargetPlayerGuid == Globals.UserID)
		{
			// IM IT!
			Gui.ScoreLabel.text = "Score: " + response.CheckInResult.TargetScore.ToString();
			Gui.TimerLabel.text = "Timer: " + response.CheckInResult.StateTimeExpiration.ToString();
			Gui.CommonLabel.text = "RUN!!!";
		}
		else
		{
			Gui.ScoreLabel.text = "Score: " + response.CheckInResult.TargetScore.ToString();
			Gui.TimerLabel.text = "Timer: " + response.CheckInResult.StateTimeExpiration.ToString();			
			foreach(PlayerLocation loc in response.CheckInResult.PlayerLocations)
			{
				if(loc.UserId == response.CheckInResult.TargetPlayerGuid)
				{
					float dis = GetDistanceFromMeTo(loc);
					Gui.CommonLabel.text = dis.ToString();
				}
			}
		}
	}
	
	private float GetDistanceFromMeTo(PlayerLocation target)
	{

		float lat1 = (float)Locator.Instance.GetLocation().latitude;
		float lat2 = (float)target.Latitude;
		float lon1 = (float)Locator.Instance.GetLocation().longitude;
		float lon2 = (float)target.Longitude;
			
		int R = 6371; // Radius of the earth in km
	  	float dLat = Mathf.Deg2Rad * (lat2-lat1);  // deg2rad below
	  	float dLon = Mathf.Deg2Rad * (lon2-lon1); 
	  	float a = 
	    Mathf.Sin(dLat/2) * Mathf.Sin(dLat/2) +
	    Mathf.Cos(Mathf.Deg2Rad *(lat1)) * Mathf.Cos(Mathf.Deg2Rad * (lat2)) * 
	    Mathf.Sin(dLon/2) * Mathf.Sin(dLon/2)
	    ; 
	  	float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1-a)); 
	  	float d = R * c; // Distance in km		
		
		return d;
	}
}
