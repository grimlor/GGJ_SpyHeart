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
        private static string _password = "r3s3tGam3";

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
            if (password == _password)
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
                        if (isHunkerDown)
                        {
                            _currentGame.CurGameState = GameState.HunkerDown;
                            _currentGame.HunkerDownExpiration = DateTime.Now.AddMinutes(2);
                        }
                        else
                        {
                            _currentGame.CurGameState = GameState.GameEnded;
                        }
                        break;
                    case GameState.HunkerDown:
                        if (DateTime.Now >= _currentGame.HunkerDownExpiration)
                        {
                            _currentGame.TargetScore++;
                        }
                        else
                        {
                            _currentGame.TrackersScore++;
                        }
                        _currentGame.CurGameState = _currentGame.TargetScore + _currentGame.TrackersScore == 5 || _currentGame.TargetScore == 3
                            ? GameState.GameEnded
                            : GameState.ActiveHunt;
                        break;
                }
            }

            return _currentGame;
        }

        public PlayersReport ViewState(string password)
        {
            return password == _password ? _currentGame : null;
        }
    }
}
