using Microsoft.AspNetCore.Mvc;
using MyToDo2.Api.Service;
using MyToDo2.Shared;
using MyToDo2.Shared.Dtos;

namespace MyToDo2.Api.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto model) => await service.LoginAsync(model.Account, model.Password);

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto model) => await service.RegisterAsync(model);
    }
}