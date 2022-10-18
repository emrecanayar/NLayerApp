using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.Core.Dtos.Requests;
using NLayerApp.Core.Services;

namespace NLayerApp.API.Controllers
{
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userService.GetAllAsync();
            return CreateActionResult(user);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto userCreateRequestDto)
        {
            var createdUser = await _userService.CreateUserAsync(userCreateRequestDto);
            return CreateActionResult(createdUser);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            return CreateActionResult(user);

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRoles(string userId)
        {
            var result = await _userService.GetRolesAsync(userId);
            return CreateActionResult(result);
        }
    }
}
