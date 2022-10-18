using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;
using NLayerApp.Service.Exceptions;
using NLayerApp.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<CustomResponseDto<UserResponseDto>> CreateUserAsync(UserCreateRequestDto userCreateRequestDto)
        {
            var user = new User { Email = userCreateRequestDto.Email, UserName = userCreateRequestDto.UserName, FirstName = userCreateRequestDto.FirstName, LastName = userCreateRequestDto.LastName };
            var result = await _userManager.CreateAsync(user, userCreateRequestDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserResponseDto>.Fail(400, errors: errors, null, false);
            }
            return CustomResponseDto<UserResponseDto>.Success(200, ObjectMapper.Mapper.Map<UserResponseDto>(user), true);
        }

        public async Task<CustomResponseDto<UserResponseDto>> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return CustomResponseDto<UserResponseDto>.Fail(404, "UserName not found", false);

            return CustomResponseDto<UserResponseDto>.Success(200, ObjectMapper.Mapper.Map<UserResponseDto>(user), true);
        }

        public async Task<CustomResponseDto<UserRoleAssignResponseDto>> GetRolesAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            UserRoleAssignResponseDto userRoleAssign = new UserRoleAssignResponseDto
            {

                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles
            };

            return CustomResponseDto<UserRoleAssignResponseDto>.Success(200, userRoleAssign, true);
        }

        public async Task<CustomResponseDto<UserListResponseDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null) throw new NotFoundException("Users Not Found");

            return CustomResponseDto<UserListResponseDto>.Success(200, new UserListResponseDto
            {
                Users = ObjectMapper.Mapper.Map<List<UserResponseDto>>(users)
            }, true);
        }
    }
}
