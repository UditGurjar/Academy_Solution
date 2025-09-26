using AcademyWeb.Infrastructure.Repositories.GenericRepository;
using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.RolesService
{
    public class RolesService : IRolesService
    {
        private readonly IGenericRepository<Role> _roleRepo;
        public RolesService(IGenericRepository<Role> roleRepo)
        {
                _roleRepo = roleRepo;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            try
            {
                return await _roleRepo.GetAllAsync();

            }
            catch (Exception)
            {

                throw;
            }        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            try
            {
                return await _roleRepo.GetByIdAsync(id);

            }
            catch (Exception)
            {

                throw;
            }      
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            try
            {
                return await _roleRepo.AddAsync(role);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            var existingRole = await _roleRepo.GetByIdAsync(role.Id);
            if (existingRole == null)
                throw new Exception("Role not found");

            existingRole.RoleName = role.RoleName;
            return await _roleRepo.UpdateAsync(existingRole);
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            return await _roleRepo.DeleteAsync(id);
        }
    }
}
