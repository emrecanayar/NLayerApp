using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllAsync();
            return CreateActionResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Assign(UserRoleAssignRequestDto userRoleAssignRequestDto)
        {
            var result = await _roleService.AssignAsync(userRoleAssignRequestDto);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestDto createRoleRequestDto)
        {
            var result = await _roleService.CreateAsync(createRoleRequestDto);
            return CreateActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleRequestDto updateRoleRequestDto)
        {
            var result = await _roleService.UpdateAsync(updateRoleRequestDto);
            return CreateActionResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string roleId)
        {
            var result = await _roleService.DeleteAsync(roleId);
            return CreateActionResult(result);
        }

    }
}
