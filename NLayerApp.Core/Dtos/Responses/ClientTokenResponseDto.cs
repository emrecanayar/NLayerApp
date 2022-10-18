using NLayerApp.Core.Interfaces;

namespace NLayerApp.Core.Dtos.Responses
{
    public class ClientTokenResponseDto : IDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }

    }
}
