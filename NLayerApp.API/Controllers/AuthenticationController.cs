using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    public class AuthenticationController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken(LoginRequestDto loginRequestDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginRequestDto);
            return CreateActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByClient(ClientLoginRequestDto clientLoginRequestDto)
        {
            var result = await _authenticationService.CreateTokenByClientAsync(clientLoginRequestDto);
            return CreateActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);
            return CreateActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token);
            return CreateActionResult(result);
        }
    }
}
