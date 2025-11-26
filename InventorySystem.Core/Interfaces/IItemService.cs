using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Dtos;

namespace InventorySystem.Core.Interfaces
{
    public interface IItemService
    {
        Task<Item> CreateItemAsync(CreateItemDto createItemDto);
        Task<Item?> UpdateItemAsync(int itemId, UpdateItemDto updateItemDto);
        Task<bool> DeleteItemAsync(int itemId);
        Task<Item?> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllItemsAsync(string? search = null, string? sortBy = null, bool sortDesc = false);
    }
}
