using System.ServiceModel;
using System.ServiceModel.Web;

namespace SpyHeart
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "setup/{password}")]
        bool Setup(string password);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "register/{longLat}/{longLng}")]
        PlayerLocation Register(string longLat, string longLng);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "checkIn/{guidPlayerId}/{longLat}/{longLng}/{intGameState}")]
        PlayersReport CheckIn(string guidPlayerId, string longLat, string longLng, string intGameState);
    }
}
