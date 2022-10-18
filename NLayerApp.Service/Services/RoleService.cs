using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Dtos.Responses;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Services;
using NLayerApp.Service.Exceptions;
using NLayerApp.Service.Mapping;

namespace NLayerApp.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<CustomResponseDto<RoleResponseListDto>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return CustomResponseDto<RoleResponseListDto>.Success(200, new RoleResponseListDto
            {
                Roles = roles
            }, true);
        }
        public async Task<CustomResponseDto<NoContentDto>> AssignAsync(UserRoleAssignRequestDto userRoleAssignRequestDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userRoleAssignRequestDto.UserId);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                if (!userRoles.Contains(role.Name))
                    await _userManager.AddToRoleAsync(user, role.Name);
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            await _userManager.UpdateSecurityStampAsync(user);
            return CustomResponseDto<NoContentDto>.Success(200, true);
        }
        public async Task<CustomResponseDto<RoleResponseDto>> CreateAsync(CreateRoleRequestDto createRoleRequestDto)
        {
            Role createRole = ObjectMapper.Mapper.Map<Role>(createRoleRequestDto);
            var result = await _roleManager.CreateAsync(createRole);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<RoleResponseDto>.Fail(400, errors: errors, null, false);
            }
            return CustomResponseDto<RoleResponseDto>.Success(200, ObjectMapper.Mapper.Map<RoleResponseDto>(createRole), true);

        }
        public async Task<CustomResponseDto<RoleResponseDto>> UpdateAsync(UpdateRoleRequestDto updateRoleRequestDto)
        {
            Role role = await _roleManager.FindByIdAsync(updateRoleRequestDto.Id);
            role.Name = updateRoleRequestDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<RoleResponseDto>.Fail(400, errors: errors, null, false);
            }
            return CustomResponseDto<RoleResponseDto>.Success(200, ObjectMapper.Mapper.Map<RoleResponseDto>(role), true);

        }
        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(string roleId)
        {
            Role role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) throw new NotFoundException("Role Not Found!");

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<NoContentDto>.Fail(400, errors: errors, null, false);
            }

            return CustomResponseDto<NoContentDto>.Success(200, true);
        }
    }
}
