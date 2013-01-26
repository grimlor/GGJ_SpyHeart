using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpyHeart
{
    [DataContract]
    public class Player
    {
        public Player(string gameId, string userId, string lat, string lng)
        {
            GameId = new Guid(gameId);
            UserId = new Guid(userId);
            Latitude = long.Parse(lat);
            Longitude = long.Parse(lng);
            IsTarget = false;
        }

        public Player(Guid gameId, Guid userId, long lat, long lng)
        {
            GameId = gameId;
            UserId = userId;
            Latitude = lat;
            Longitude = lng;
            IsTarget = false;
        }

        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public int AgentNumber { get; set; }

        [DataMember]
        public long Latitude { get; set; }

        [DataMember]
        public long Longitude { get; set; }

        [DataMember]
        public bool IsTarget { get; set; }
    }
}
