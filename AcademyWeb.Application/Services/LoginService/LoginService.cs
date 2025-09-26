using AcademyWeb.Application.Helpers;
using AcademyWeb.Infrastructure.Repositories.LoginRepository;
using AcademyWeb.Models;
using AcademyWeb.Models.DTOs;
using AcademyWeb.Models.QueryModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }
        public async Task<AuthResponseDTO?> AuthenticateUser(UserLoginQuery user)
        {
            try
            {
                var userDetails = await _loginRepository.VerifyUser(user.UserName);

                if (userDetails == null) return null;
                var verifyPassword = PasswordHelper.VerifyPassword(user.Password, userDetails.PasswordHash, "SHA1");
                if (!verifyPassword) return null;
                var claims = new[]
                 {
                new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()),
                new Claim(ClaimTypes.Name, userDetails.Username),
                new Claim(ClaimTypes.Role, userDetails.Role.RoleName.ToString()),
                new Claim("RoleId",userDetails.RoleId.ToString())
            };
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                    signingCredentials: creds);

                return new AuthResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    User = userDetails
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
