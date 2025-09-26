using AcademyWeb.Models;
using AcademyWeb.Models.DTOs;
using AcademyWeb.Models.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.LoginService
{
    public interface ILoginService
    {
        Task<AuthResponseDTO> AuthenticateUser(UserLoginQuery user);

    }
}
