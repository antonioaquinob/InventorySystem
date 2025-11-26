using Microsoft.EntityFrameworkCore;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Entities;
using InventorySystem.API.Data;
namespace InventorySystem.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InventoryDbContext _context;
        public UserRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllUserAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Task.FromResult(user);
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var findUser = await _context.Users.FindAsync(user.UserId);
            if (findUser == null)
                return false;
            _context.Users.Remove(findUser);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
