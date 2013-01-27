using System.ServiceModel;
using System.ServiceModel.Web;

namespace SpyHeart
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "register/{longLat}/{longLng}")]
        PlayerLocation Register(string longLat, string longLng);

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "checkIn/{guidPlayerId}/{longLat}/{longLng}/{intGameState}")]
        PlayersReport CheckIn(string guidPlayerId, string longLat, string longLng, string intGameState);
    }
}
