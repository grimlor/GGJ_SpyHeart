using System;
using System.Runtime.Serialization;

namespace SpyHeart
{
    [DataContract]
    public class PlayerLocation
    {
        public PlayerLocation(Guid userId, double lat, double lng)
        {
            UserId = userId;
            Latitude = lat;
            Longitude = lng;
        }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }
    }
}
