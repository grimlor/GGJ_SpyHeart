using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SpyHeart
{
    [ServiceContract]
    public interface IFindMeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "register/{guidGameId}/{longLat}/{longLng}")]
        Player Register(string guidGameId, string longLat, string longLng);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "getServers")]
        IList<Game> GetServers();

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "hereIAm/{guidGameId}/{guidUserId}/{longLat}/{longLng}")]
        IList<Player> HereIAm(string guidGameId, string guidUserId, string longLat, string longLng);
    }
}
