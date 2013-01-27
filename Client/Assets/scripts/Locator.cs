using UnityEngine;
using System.Collections;

public class Locator : MonoBehaviour
{
	public delegate void OnLocationServiceInitialized(LocationResult result);
	
	public static Locator Instance
	{
		get{
			GameObject locator = GameObject.Find("LOCATOR");
			return (Locator)locator.GetComponent(typeof(Locator));
		}
	}
	
	public class LocationResult
	{
		public bool Success;
		public string Message;
		
		public LocationResult(bool success, string message)
		{
			this.Success = success;
			this.Message = message;
		}
	}
	
	private OnLocationServiceInitialized _callback = null;
	private float _maxWait = 20f;
	
	public void InitializeLocationServices(OnLocationServiceInitialized callback)
	{
		_maxWait = 20f;
		_callback = callback;
		
		// Start service before querying location
		Input.location.Start(5f, 5f);
	}
	
	private void Update()
	{
		if(_callback != null)
		{
		    // First, check if user has location service enabled
		    if (!Input.location.isEnabledByUser)
			{
				_callback(new LocationResult(false, "Location Service Not Enabled"));
				_callback = null;
				return;
			}
			
			_maxWait -= Time.deltaTime;
		    if(_maxWait < 0f)
			{
				_callback(new LocationResult(false, "Location service didn't initialize in 20 seconds"));
				_callback = null;
				return;
			}
			
		    if(Input.location.status != LocationServiceStatus.Initializing)
			{
			    if (Input.location.status == LocationServiceStatus.Failed)
				{
					_callback(new LocationResult(false, "Location service failed"));
					_callback = null;
					return;
				}
				
			    _callback(new LocationResult(true, "Success"));
				_callback = null;
				return;
		    }
		}
	}
	
	public LocationInfo GetLocation()
	{
		return Input.location.lastData;
	}
	
	IEnumerator InitialWait()
	{
		yield return new WaitForSeconds(5);
	}
	
	IEnumerator WaitForService()
	{
        yield return new WaitForSeconds(1);
    }
}
