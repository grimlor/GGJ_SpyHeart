﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SpyHeart
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FindMeService : IFindMeService
    {
        private static IList<Player> _players;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindMeService"/> class.
        /// </summary>
        public FindMeService()
        {
            _players = new List<Player>();
        }

        /// <summary>
        /// Registers a user to a game.
        /// </summary>
        /// <param name="guidGameId">The game id.</param>
        /// <param name="longLat">The long lat.</param>
        /// <param name="longLng">The long LNG.</param>
        /// <returns></returns>
        public Player Register(string guidGameId, string longLat, string longLng)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit current position.
        /// </summary>
        /// <param name="guidGameId">The game id.</param>
        /// <param name="guidUserId">The user id.</param>
        /// <param name="intLat">The latitude.</param>
        /// <param name="intLng">The longitude.</param>
        /// <returns></returns>
        public IList<Player> HereIAm(string guidGameId, string guidUserId, string intLat, string intLng)
        {
            List<Player> playersInGame;

            try
            {
                var gameId = new Guid(guidGameId);
                var userId = new Guid(guidUserId);
                var lat = long.Parse(intLat);
                var lng = long.Parse(intLng);

                Player player;
                if (!(_players.Any(p => p.GameId.Equals(gameId) && p.UserId.Equals(userId))))
                {
                    player = new Player(gameId, userId, lat, lng);
                    _players.Add(player);
                }
                else
                {
                    player = _players.Single(p => p.GameId.Equals(gameId));
                    player.Latitude = lat;
                    player.Longitude = lng;
                }

                playersInGame = _players.Where(p => p.GameId.Equals(gameId)).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid parameters were used.");
                playersInGame = new List<Player>();
            }

            return playersInGame;
        }


        public IList<Game> GetServers()
        {
            throw new NotImplementedException();
        }
    }
}
