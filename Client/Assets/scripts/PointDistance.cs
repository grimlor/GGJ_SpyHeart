using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	float lat1 = 82.0857f;
	float lat2 = 83.9826749f;
	float lon1 = -31.32946f;
	float lon2 = -31.679490f;
		
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
}
	
	// Update is called once per frame
	void Update () {
	
	}
}
