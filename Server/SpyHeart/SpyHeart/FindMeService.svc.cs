using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SpyHeart
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FindMeService : IFindMeService
    {
        private static PlayersReport _currentGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindMeService"/> class.
        /// </summary>
        public FindMeService()
        {
            _currentGame = new PlayersReport { PlayerLocations = new List<PlayerLocation>(), CurGameState = GameState.PreLaunch };
        }

        public PlayerLocation Register(string longLat, string longLng)
        {
            var lat = long.Parse(longLat);
            var lng = long.Parse(longLng);
            var playerLocation = new PlayerLocation(Guid.NewGuid(), lat, lng);

            _currentGame.PlayerLocations.Add(playerLocation);

            return playerLocation;
        }

        public PlayersReport CheckIn(string guidPlayerId, string longLat, string longLng, string intGameState)
        {
            var userId = new Guid(guidPlayerId);
            var lat = long.Parse(longLat);
            var lng = long.Parse(longLng);
            var gameState = (GameState)int.Parse(intGameState);

            var playerToUpdate = _currentGame.PlayerLocations.Single(p => p.UserId == userId);
            playerToUpdate.Latitude = lat;
            playerToUpdate.Longitude = lng;

            // check for state change

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
