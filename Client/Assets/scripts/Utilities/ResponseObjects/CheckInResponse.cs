using UnityEngine;
using System.Collections;

public class CheckInResponse
{
	public CheckInResult CheckInResult;
	
	public override string ToString ()
	{
		string res = "CheckInResponse:\nPlayerLocs(" + CheckInResult.PlayerLocations.Length + ")\n";
		
		for(int i = 0; i < CheckInResult.PlayerLocations.Length; i++)
		{
			res += "{Lat:" + CheckInResult.PlayerLocations[i].Latitude +
				", Lon:" + CheckInResult.PlayerLocations[i].Longitude + 
				", ID:" + CheckInResult.PlayerLocations[i].UserId + "}\n";
		}
		
		res += "GameState: " + CheckInResult.CurGameState + "\n";
		res += "StateTimeExpiration: " + CheckInResult.StateTimeExpiration + "\n";
		res += "TargetPlayerGuid: " + CheckInResult.TargetPlayerGuid + "\n";
		res += "TargetScore: " + CheckInResult.TargetScore + "\n";
		res += "TrackersScore: " + CheckInResult.TrackersScore;
		
		return res;
	}
}
