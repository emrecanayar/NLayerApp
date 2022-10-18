using NLayerApp.Core.Interfaces;

namespace NLayerApp.Core.Dtos.Responses
{
    public class TokenResponseDto : IDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
