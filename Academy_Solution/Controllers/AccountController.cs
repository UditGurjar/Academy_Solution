using AcademyWeb.Application.Helpers;
using AcademyWeb.Application.Services.LoginService;
using AcademyWeb.Application.Services.UserService;
using AcademyWeb.Models;
using AcademyWeb.Models.DTOs;
using AcademyWeb.Models.QueryModels;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AcademyWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        public AccountController(ILoginService loginService,IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginQuery userLoginQuery)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(userLoginQuery.UserName) || string.IsNullOrEmpty(userLoginQuery.Password)) return BadRequest("Please Enter Valid Credentials");
                    var response =await _loginService.AuthenticateUser(userLoginQuery);
                    if (response == null) return Unauthorized("Invalid credentials");
                    return Ok(response);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Error comes while login",ex.Message);
                return BadRequest(ex);
            }
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto == null) return BadRequest();
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = dto.Username,
                        Email = dto.Email,
                        PasswordHash = dto.Password, 
                        RoleId = dto.RoleId,
                        CreatedDate = DateTime.UtcNow
                    };
                    var result = await _userService.CreateUserAsync(user);
                    if(result == null) return BadRequest(); 
                    return Ok(result);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error comes while login", ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
