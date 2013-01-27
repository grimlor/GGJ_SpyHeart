using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SpyHeart
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : IGameService
    {
        private static PlayersReport _currentGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService()
        {
            _currentGame = new PlayersReport
            {
                PlayerLocations = new List<PlayerLocation>(),
                CurGameState = GameState.PreLaunch
            };
        }

        public bool Setup(string password)
        {
            var isGameReset = false;
            if (password == "r3s3tGam3")
            {
                _currentGame = new PlayersReport
                {
                    PlayerLocations = new List<PlayerLocation>(),
                    CurGameState = GameState.PreLaunch
                };
                isGameReset = true;
            }
            return isGameReset;
        }

        public PlayerLocation Register(string longLat, string longLng)
        {
            var lat = double.Parse(longLat.Replace('d', '.'));
            var lng = double.Parse(longLng.Replace('d', '.'));
            var playerLocation = new PlayerLocation(Guid.NewGuid(), lat, lng);

            _currentGame.PlayerLocations.Add(playerLocation);
            if (_currentGame.PlayerLocations.Count == 1)
            {
                _currentGame.StateTimeExpiration = DateTime.Now.AddMinutes(2);
            }

            return playerLocation;
        }

        public PlayersReport CheckIn(string guidPlayerId, string longLat, string longLng, string intGameState)
        {
            var userId = new Guid(guidPlayerId);
            var lat = double.Parse(longLat.Replace('d', '.'));
            var lng = double.Parse(longLng.Replace('d', '.'));
            var isHunkerDown = (GameState)int.Parse(intGameState) == GameState.HunkerDown;

            var playerToUpdate = _currentGame.PlayerLocations.Single(p => p.UserId == userId);
            playerToUpdate.Latitude = lat;
            playerToUpdate.Longitude = lng;

            if (DateTime.Now >= _currentGame.StateTimeExpiration)
            {
                switch (_currentGame.CurGameState)
                {
                    case GameState.PreLaunch:
                        var random = new Random();
                        _currentGame.TargetPlayerGuid = _currentGame.PlayerLocations[random.Next(0, _currentGame.PlayerLocations.Count - 1)].UserId;
                        _currentGame.CurGameState = GameState.CountTo100;
                        _currentGame.StateTimeExpiration = DateTime.Now.AddMinutes(2);
                        break;
                    case GameState.CountTo100:
                        _currentGame.CurGameState = GameState.ActiveHunt;
                        _currentGame.StateTimeExpiration = DateTime.Now.AddDays(1);
                        break;
                    case GameState.ActiveHunt:
                        _currentGame.CurGameState = GameState.GameEnded;
                        break;
                    case GameState.HunkerDown:
                        _currentGame.CurGameState = GameState.ActiveHunt;
                        _currentGame.StateTimeExpiration = DateTime.Now.AddMinutes(2);
                        break;
                }
            }

            return _currentGame;
        }
    }

    //////////////////////////////////////// Scratchspace: States & Messages //////////////////////////////////////////////////
    /*
     * [Setup Game]
     * 		<PlayerGUID> SetupPlayer( string playerDeviceGUID, string username )
     * 		<GameGUID> SetupGame( string ownerDeviceGUID, string ownerUsername, string epicenterLat, string epicenterLong, string startTime, string duration )
     * [Register Players]
     * 		<StartTimes&
     * 
     * 
     **************************************** [Count to 100] **************************************************
     * 
     * 	Struct: PlayerLocation { playerGUID, playerLatt, playerLong }
     * 	
     * 	Struct: PlayersReport { targetPlayerGUID, playerLocations[], countdownTime, curGameState, trackersScore, targetScore }
     * 	
     * 	playersReport CheckIn( string playerGUID, string latt, string long, string curGameState, string newGameState )
     * 	{
     * 		// Convert string params to actionable data
     * 		updatePlayerPosition( playerGUID, latt, long);
     * 		switch ( this.state )
     * 		{
     * 			case COUNT_TO_100: return processCountTo100State( playerGUID, latt, long );
     * 			case ACTIVE_HUNT: return processActiveHuntState();
     * 			default: // Throw error
     * 		}
     * 	}
     * 	
     * 	playersReport processCountTo100State( playerGUID, latt, long ) {
     * 		if not game.active {
     * 			// Calculate Count to 100 duration timestamp
     * 			game.active = true;
     * 		}
     * 		if isElapsed( duration ) {
     * 			// Set game state to Active Hunt.
     * 		}
     * 		return constructPlayersReport();
     * 	}
     * 
     * 	void updatePlayerPosition( playerGUID, latt, long ) { // Update player position in internal data model }
     * 	
     * 	playersReport constructPlayersReport() { // Package internal data model for transport }
     * 
     **************************************** [Active Hunt] **************************************************
     * 	
     * 	playersReport processActiveHunt( playerGUID, latt, long, curGameState, newGameState )
     * 	{
     * 	}
     */
}
