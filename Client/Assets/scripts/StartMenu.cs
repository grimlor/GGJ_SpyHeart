using UnityEngine;
using System.Collections;
using Utilities;

public class StartMenu : MonoBehaviour {
	
	private void Start()
	{
		Gui.DebuggingLabel = GameObject.Find("Debug Label").GetComponent<UILabel>();
		Gui.DebuggingLabel.text = "";
	}
	
	void TargetClicked() 
	{	
		Gui.DebuggingLabel.text = "Updated";
		Locator.Instance.InitializeLocationServices(onLocationServerInitialized);
	}
	
	public void onLocationServerInitialized(Locator.LocationResult result)
	{
		Debug.Log(result.Message);
		
		if (result.Success)
		{
			try {
				API.Instance.Call("Register", onRegisterCompleted, Locator.Instance.GetLocation().latitude, Locator.Instance.GetLocation().longitude);
			} catch (System.Exception ex) {
				Gui.DebuggingLabel.text = ex.Message;
			}
		}
		else
		{
			Gui.DebuggingLabel.text = result.Message;
		}
	}
	
	// TODO: Change to two callbacks: OnRegistered Callback & OnHeartbeat
	public void onRegisterCompleted(APIEvent e)
	{
		Debug.Log("Got callback");
		
		try {
			RegisterResponse response = Parsing.json_decode<RegisterResponse>(e.Response);
			
			Globals.UserID = response.RegisterResult.UserId;
			
			Application.LoadLevel("Game");
		} catch (System.Exception ex) {
			Gui.DebuggingLabel.text = ex.Message;
		}
		
		
	}	
	
}
