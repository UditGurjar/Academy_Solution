using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Infrastructure.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        Task<User> VerifyUser(string username);
    }
}
