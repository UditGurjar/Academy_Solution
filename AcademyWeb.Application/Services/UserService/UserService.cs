using AcademyWeb.Application.Helpers;
using AcademyWeb.Infrastructure.Repositories.GenericRepository;
using AcademyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyWeb.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));
                var hashPassword = PasswordHelper.ComputeHash(user.PasswordHash, "SHA1", null);
                if (string.IsNullOrEmpty(hashPassword)) return null;
                user.PasswordHash = hashPassword;
                user.CreatedDate = DateTime.Now;
                return await _userRepository.AddAsync(user);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid user Id", nameof(id));

                return await _userRepository.DeleteAsync(id);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Invalid user Id", nameof(id));

                return await _userRepository.GetByIdAsync(id);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var existingUser = await _userRepository.GetByIdAsync(user.Id);
                if (existingUser == null)
                    throw new KeyNotFoundException($"User with Id {user.Id} not found");

                // Update fields
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash;


                return await _userRepository.UpdateAsync(existingUser);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
