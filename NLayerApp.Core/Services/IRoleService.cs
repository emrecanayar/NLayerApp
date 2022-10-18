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
    public interface IRoleService
    {
        Task<CustomResponseDto<RoleResponseListDto>> GetAllAsync();
        Task<CustomResponseDto<RoleResponseDto>> CreateAsync(CreateRoleRequestDto createRoleRequestDto);
        Task<CustomResponseDto<RoleResponseDto>> UpdateAsync(UpdateRoleRequestDto updateRoleRequestDto);
        Task<CustomResponseDto<NoContentDto>> DeleteAsync(string roleId);
        Task<CustomResponseDto<NoContentDto>> AssignAsync(UserRoleAssignRequestDto userRoleAssignRequestDto);

    }
}
