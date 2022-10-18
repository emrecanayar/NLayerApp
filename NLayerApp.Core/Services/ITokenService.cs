using NLayerApp.Core.Configurations;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Entities;

namespace NLayerApp.Core.Services
{
    public interface ITokenService
    {
        TokenResponseDto CreateToken(User user, IList<string> userRoles);
        ClientTokenResponseDto CreateTokenByClient(Client client);
    }
}
