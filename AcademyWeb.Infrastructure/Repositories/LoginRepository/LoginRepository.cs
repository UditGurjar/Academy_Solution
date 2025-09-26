using AcademyWeb.Infrastructure.DBContext;
using AcademyWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Infrastructure.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> VerifyUser(string username)
        {
            try
            {
                return await _context.Users.Include(x=>x.Role)
                       .FirstOrDefaultAsync(x => x.Email == username);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
