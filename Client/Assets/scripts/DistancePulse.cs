using UnityEngine;
using System.Collections;

public class DistancePulse : MonoBehaviour {
	
	
			public static int PULSE_DISTANCE_MOD_MAX = 100;
		public static int PULSE_DISTANCE_MOD_MIN = 0;
		public static int MAXIMUM_RANGE_KM = 1;
		//private int pulseDistModLevelPrevious = 0;

	
	
	// Use this for initialization
	void Start () {
		

		
		 
	}
		
		
		void adjustHeartRate(float distCurrent){
			
			//test data
			//float distCurrent = .48974924f;
			
		
			float maxRange = MAXIMUM_RANGE_KM;
			int pulseDistModMax = PULSE_DISTANCE_MOD_MAX;
			int pulseDistModMin = PULSE_DISTANCE_MOD_MIN;
			float pulseDistIncrement = maxRange / pulseDistModMax;
			int stratifiedDistanceFromTarget = (int) (distCurrent / pulseDistModMax * pulseDistIncrement);

			

		}
		
		
	
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
