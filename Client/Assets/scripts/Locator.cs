using UnityEngine;
using System.Collections;

public class Locator : MonoBehaviour {
	
	private UILabel _displayLabel;
	private int _maxWait = 20;
	

	void Start () {
		_displayLabel = GameObject.Find("Display Label").GetComponent<UILabel>();
		_displayLabel.text = "";
		initializeLocationServices();
		InvokeRepeating("GetLocation", 5f, 5f);
	}
	
	void Update () {

	}
	
	private void initializeLocationServices(){
	    // First, check if user has location service enabled
	    if (!Input.location.isEnabledByUser)
		{
	     	_displayLabel.text = "Location Service Not Enabled";
			return;
		}
	
	    // Start service before querying location
	    Input.location.Start(5f, 5f);
	
	    // Wait until service initializes
	    while (Input.location.status == LocationServiceStatus.Initializing && _maxWait > 0) {
	        WaitForService();
	        _maxWait--;
	    }
	
	    // Service didn't initialize in 20 seconds
	    if (_maxWait < 1) {
	        _displayLabel.text = "Location service didn't initialize in 20 seconds";
	        return;
	    }
		
	    // Stop service if there is no need to query location updates continuously
	    //Input.location.Stop ();		
	}
	
	private void GetLocation()
	{
	    if (Input.location.status == LocationServiceStatus.Failed) {
	        _displayLabel.text = "Unable to determine device location";
	        return;
	    }
	    // Access granted and location value could be retrieved
	    else {
	        _displayLabel.text = 
				"Lat: " + Input.location.lastData.latitude + "\n" +
	            "Long: " + Input.location.lastData.longitude + "\n" +
	            "Alt: " + Input.location.lastData.altitude + "\n" +
	            "Accur: " + Input.location.lastData.horizontalAccuracy + "\n" +
	            "Time: " + Input.location.lastData.timestamp + "\n" + 
				"LoopTime: " + Time.time;
	    }	
	
	    StartCoroutine(SendLocation());		
	}
	
	IEnumerator WaitForService() {
        yield return new WaitForSeconds(1);
    }
	
    IEnumerator SendLocation()
    {
		var form = new WWWForm();
		
        if (Application.internetReachability == NetworkReachability.NotReachable)
            yield break;

        var url = "http://www.mygamehud.com/api/v2/";
        url += method;

        form.AddField("gh_api_key", gameApiKey);
		form.AddField("gh_device_identifier", DeviceIdentifier);
		form.AddField("gh_submitted_at", DateTime.Now.ToString("O"));

        WWW www = new WWW(url, form);
        yield return www;

        // WWW does not react to HTTP status codes, only transport errors?
        if (www.error != null) // || www.text.Substring(0, 1) != "0")
        {
            Debug.Log(www);
        }
        else 
        {

        }
    }	
}
