using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpyHeart
{
    [DataContract]
    public class PlayersReport
    {
        [DataMember]
        public Guid TargetPlayerGuid;

        [DataMember]
        public IList<PlayerLocation> PlayerLocations;

        [DataMember]
        public DateTime StateTimeExpiration;

        [DataMember]
        public GameState CurGameState;

        [DataMember]
        public int TrackersScore;

        [DataMember]
        public int TargetScore;

        public DateTime HunkerDownExpiration;
    }
}
