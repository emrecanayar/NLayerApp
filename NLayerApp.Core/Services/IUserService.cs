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
    public interface IUserService
    {
        Task<CustomResponseDto<UserListResponseDto>> GetAllAsync();
        Task<CustomResponseDto<UserResponseDto>> CreateUserAsync(UserCreateRequestDto userCreateRequestDto);
        Task<CustomResponseDto<UserResponseDto>> GetUserByUserNameAsync(string userName);
        Task<CustomResponseDto<UserRoleAssignResponseDto>> GetRolesAsync(string userId);
    }
}
