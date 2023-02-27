using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Core;

namespace ScheduleManagementSession01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
   {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserCreateModel model)
        {
            var result = await _userService.Register(model);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = _userService.GetAll();
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(model);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
    }
}
