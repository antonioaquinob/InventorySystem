using InventorySystem.API.Data;
using InventorySystem.Core.Dtos;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BCrypt.Net;
namespace InventorySystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly InventoryDbContext _context;
        public UserService(IUserRepository userRepository, InventoryDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<User?> GetItemByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(string? search = null, string? sortBy = null, bool sortDesc = false)
        {
            IQueryable<User> queryable = _context.Users;

            if(!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                queryable = queryable.Where(b => b.UserName.ToLower().Contains(search));
            }

            if(!string.IsNullOrEmpty(sortBy))
            {
                queryable = sortBy.ToLower() switch
                {
                    "username" => sortDesc ? queryable.OrderByDescending(b => b.UserName) : queryable.OrderBy(b => b.UserName),
                    _ => queryable
                };
            }
            return await queryable.ToListAsync();
        }

        public async Task<User> CreateUser(CreateUserDto dto)
        {
            try
            {
                var user = new User
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = dto.Role
                };
                await _userRepository.CreateUser(user);
                await _userRepository.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<User> UpdateUser(int userId, UpdateUserDto dto)
        {
            try
            {
                var findUser =  await _userRepository.GetUserByIdAsync(userId);
                if (findUser == null)
                    return null;

                findUser.UserName = dto.UserName;
                findUser.Email = dto.Email;
                // If user wants to update password
                if (!string.IsNullOrEmpty(dto.PasswordHash))
                {
                    findUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
                }

                findUser.Role = dto.Role;
                await _userRepository.UpdateUser(findUser);
                await _userRepository.SaveChangesAsync();
                return findUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var findUser = await _userRepository.GetUserByIdAsync(userId);
                if(findUser == null)
                    return false;

                await _userRepository.DeleteUserAsync(findUser);
                await _userRepository.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
