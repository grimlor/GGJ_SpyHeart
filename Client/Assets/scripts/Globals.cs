using UnityEngine;
using System.Collections;

public class Globals
{
	public enum GameState {
		NewPlayer = 0,
		PreLaunch,
		CountTo100,
		ActiveHunt,
		HunkerDown,
		GameEnded
	};
	
	public static string UserID = "";
	
	public static GameState CurGameState = GameState.NewPlayer;

}
