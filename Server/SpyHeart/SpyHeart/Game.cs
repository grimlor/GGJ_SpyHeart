using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpyHeart
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid GameId { get; set; }

        [DataMember]
        public IList<PlayerLocation> Players { get; set; }
    }
}
