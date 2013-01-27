using UnityEngine;
using System.Collections;

public class Locator : MonoBehaviour {
	
	private UILabel _commonLabel;
	private int _maxWait = 20;
	

	void Start () {
		_commonLabel = GameObject.Find("Common Label").GetComponent<UILabel>();
		_commonLabel.text = "";
		initializeLocationServices();
		InvokeRepeating("GetLocation", 5f, 5f);
	}
	
	void Update () {

	}
	
	private void initializeLocationServices(){
	    // First, check if user has location service enabled
	    if (!Input.location.isEnabledByUser)
		{
	     	_commonLabel.text = "Location Service Not Enabled";
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
	        _commonLabel.text = "Location service didn't initialize in 20 seconds";
	        return;
	    }
		
	    // Stop service if there is no need to query location updates continuously
	    //Input.location.Stop ();		
	}
	
	private void GetLocation()
	{
	    if (Input.location.status == LocationServiceStatus.Failed) {
	        _commonLabel.text = "Unable to determine device location";
	        return;
	    }
	    // Access granted and location value could be retrieved
	    else {
	        //_displayLabel.text = 
				//"Lat: " + Input.location.lastData.latitude + "\n" +
	            //"Long: " + Input.location.lastData.longitude + "\n";// +
	            //"Alt: " + Input.location.lastData.altitude + "\n" +
	            //"Accur: " + Input.location.lastData.horizontalAccuracy + "\n" +
	            //"Time: " + Input.location.lastData.timestamp + "\n" + 
				//"LoopTime: " + Time.time;
	    }	
	
	    StartCoroutine(SendLocation());		
	}
	
	IEnumerator WaitForService() {
        yield return new WaitForSeconds(1);
    }
	
    IEnumerator SendLocation()
    {
		//var form = new WWWForm(); 
		
        if (Application.internetReachability == NetworkReachability.NotReachable)
            yield break;

        var url = "http://ggjspyheart2.cloudapp.net/FindMeService.svc/HereIAm/"; //<guidGameId>/<guidUserId>/<longLat>/<longLng>";
        url += "9ef0941f-38c4-448d-ad76-7a8c59837535" + "/";
        url += "6c2711a9-da5b-42da-ad06-21f9fda2c97a" + "/";
		url += Input.location.lastData.latitude.ToString() + "/";
		url += Input.location.lastData.longitude.ToString();
		
		//Debug.Log(url);
        //form.AddField("gh_api_key", gameApiKey);

        WWW www = new WWW(url);
        yield return www;
		
		Debug.Log(www.text);
		
		//MiniJSON.jsonDecode(www.text);
		
        // WWW does not react to HTTP status codes, only transport errors?
        if (www.error != null) // || www.text.Substring(0, 1) != "0")
        {
            Debug.Log(www.text);
        }
        else 
        {
			_commonLabel.text = www.text.ToString();
        }
    }	
}
