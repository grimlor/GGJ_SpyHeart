using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
        public int CountdownTime;

        [DataMember]
        public GameState CurGameState;

        [DataMember]
        public int TrackersScore;

        [DataMember]
        public int TargetScore;
    }
}
