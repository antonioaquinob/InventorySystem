using InventorySystem.Core.Dtos;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using InventorySystem.API.Data;
namespace InventorySystem.API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly InventoryDbContext _context;
        public ItemRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        public Task<Item> CreateItem(Item item)
        {
            _context.Items.Add(item);
            return Task.FromResult(item);
        }
        
        public Task<Item> UpdateItem(Item item)
        {
            _context.Items.Update(item);
            return Task.FromResult(item);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var findItem = await _context.Items.FindAsync(item.ItemId);
            if (findItem == null)
                return false;

            _context.Items.Remove(findItem);
            return true;

        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
