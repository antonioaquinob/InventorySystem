using InventorySystem.Core.Dtos;
using InventorySystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetItemByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUserAsync(string? search = null, string? sortBy = null, bool sortDesc = false);
        Task<User> CreateUser(CreateUserDto dto);
        Task<User> UpdateUser(int userId, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
