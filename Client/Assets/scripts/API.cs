using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class API : MonoBehaviour
{
	public delegate void APICallback (APIEvent e);
	
	//private Queue<APICallback> _callbacks = new Queue<APICallback>();
	private APICallback _currCallback;
	private Guid _myId;
	
	public static API Instance
	{
		get{
			GameObject api = GameObject.Find("API");
			return (API)api.GetComponent(typeof(API));
		}
	}
	
	public void Call(string method, APICallback callback, params object[] args)
	{
		if(_currCallback == null)
		{
			_currCallback = callback;
			
			string url = "http://ggjspyheart2.cloudapp.net/GameService.svc/" + method;
			
			foreach(object obj in args)
				url += ("/" + obj.ToString());
			
			Debug.Log (url);
			StartCoroutine(SendLocation(url));
		}
		else
			throw new Exception("NOPE!");
	}
	
	IEnumerator SendLocation(string url)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            yield break;

        WWW www = new WWW(url);
        yield return www;
		
		Debug.Log(www.text);
		
        if (www.error != null)
			_currCallback(new APIEvent(false, www.error));
        else
			_currCallback(new APIEvent(true, www.text));
		
		_currCallback = null;
    }
}
