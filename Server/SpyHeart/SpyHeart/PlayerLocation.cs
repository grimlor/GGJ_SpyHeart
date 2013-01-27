using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpyHeart
{
    [DataContract]
    public class PlayerLocation
    {
        public PlayerLocation(Guid userId, long lat, long lng)
        {
            UserId = userId;
            Latitude = lat;
            Longitude = lng;
        }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public long Latitude { get; set; }

        [DataMember]
        public long Longitude { get; set; }
    }
}
