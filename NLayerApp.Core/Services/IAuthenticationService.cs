using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenResponseDto>> CreateTokenAsync(LoginRequestDto loginRequestDto);
        Task<CustomResponseDto<TokenResponseDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);
        Task<CustomResponseDto<ClientTokenResponseDto>> CreateTokenByClientAsync(ClientLoginRequestDto clientLoginRequestDto);
    }
}
