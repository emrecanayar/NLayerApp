using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Repositories;
using NLayerApp.Core.Services;
using NLayerApp.Core.UnitOfWorks;

namespace NLayerApp.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {

            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<CustomResponseDto<TokenResponseDto>> CreateTokenAsync(LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto == null) throw new ArgumentNullException(nameof(loginRequestDto));

            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (user == null) return CustomResponseDto<TokenResponseDto>.Fail(400, "Email or Password is wrong", false);

            if (!await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
                return CustomResponseDto<TokenResponseDto>.Fail(400, "Email or Password is wrong", false);

            var token = _tokenService.CreateToken(user, userRoles);
            var userRefreshToken = await _userRefreshTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Token = token.RefreshToken, TokenExpireDate = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.TokenExpireDate = token.RefreshTokenExpiration;
            }

            await _unitOfWork.SaveAsync();
            return CustomResponseDto<TokenResponseDto>.Success(200, token, true);

        }

        public Task<CustomResponseDto<ClientTokenResponseDto>> CreateTokenByClientAsync(ClientLoginRequestDto clientLoginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<TokenResponseDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return CustomResponseDto<TokenResponseDto>.Fail(404, "Refresh token not found", false);

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user == null) return CustomResponseDto<TokenResponseDto>.Fail(404, "User Id not found", false);

            var tokenDto = _tokenService.CreateToken(user, userRoles);
            existRefreshToken.Token = tokenDto.RefreshToken;
            existRefreshToken.TokenExpireDate = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.SaveAsync();
            return CustomResponseDto<TokenResponseDto>.Success(200, tokenDto, true);

        }

        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null) return CustomResponseDto<NoContentDto>.Fail(404, "Refresh token not found", false);

            _userRefreshTokenRepository.Remove(existRefreshToken);
            await _unitOfWork.SaveAsync();

            return CustomResponseDto<NoContentDto>.Success(200, true);
        }
    }
}
