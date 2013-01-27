using UnityEngine;
using System.Collections;

public class APIEvent
{
	public bool Success;
	public string Response;
	
	
	public APIEvent(bool success, string response)
	{
		this.Success = success;
		this.Response = response;
	}
}
