using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Core.Entities;
namespace InventorySystem.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Item>> GetAllUserAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUserAsync(User user);
        Task SaveChangesAsync();
    }
}
