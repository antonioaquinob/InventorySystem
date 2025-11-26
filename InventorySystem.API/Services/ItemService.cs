using InventorySystem.Core.Entities;
using InventorySystem.Core.Dtos;
using InventorySystem.Core.Interfaces;
using InventorySystem.API.Data;
using InventorySystem.API.Repositories;
using Microsoft.EntityFrameworkCore;
namespace InventorySystem.API.Services
{

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly InventoryDbContext _context;
        public ItemService(IItemRepository itemRepository, InventoryDbContext context)
        {
            _itemRepository = itemRepository;
            _context = context;
        }

        public async Task<Item> CreateItemAsync(CreateItemDto createItemDto)
        {
            try
            {
                var item = new Item
                {
                    ItemName = createItemDto.ItemName,
                    ItemBrand = createItemDto.ItemBrand,
                    ItemDescription = createItemDto.ItemDescription,
                    ItemPrice = createItemDto.ItemPrice,
                    QuantityAvailable = createItemDto.QuantityAvailable,
                    CriticalLevel = createItemDto.CriticalLevel
                };
                await _itemRepository.CreateItemAsync(item);
                await _itemRepository.SaveChangesAsync();
                return item;
            }
            catch(Exception)
            {
                // let the controller throw any error
                throw;
            }
        }

        public async Task<Item?> UpdateItemAsync(int itemId, UpdateItemDto updateItemDto)
        {
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(itemId);
                if (item == null)
                    return null;

                // update fields
                item.ItemName = updateItemDto.ItemName;
                item.ItemBrand = updateItemDto.ItemBrand;
                item.ItemDescription = updateItemDto.ItemDescription;
                item.ItemPrice = updateItemDto.ItemPrice;
                item.QuantityAvailable = updateItemDto.QuantityAvailable;
                item.CriticalLevel = updateItemDto.CriticalLevel;

                await _itemRepository.UpdateItemAsync(item);
                await _itemRepository.SaveChangesAsync();
                return item;
            }
            catch(Exception ex)
            {
                // let the controller throw any error
                throw;
            }
        }

        public async Task<bool> DeleteItemAsync(int itemId)
        {
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(itemId);
                if (item == null)
                    return false;

                await _itemRepository.DeleteItemAsync(item);
                await _itemRepository.SaveChangesAsync();
                return true;

            }
            catch(Exception ex)
            {
                // let the controller throw any error
                throw;
            }
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            return await _itemRepository.GetItemByIdAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync(string? search = null, string? sortBy = null, bool sortDesc = false)
        {
            IQueryable<Item> query = _context.Items;

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(b =>
                b.ItemName.ToLower().Contains(search) ||
                b.ItemDescription.ToLower().Contains(search) ||
                b.ItemBrand.ToLower().Contains(search));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "itemName" => sortDesc ? query.OrderByDescending(b=>b.ItemName) : query.OrderBy(b=>b.ItemName),
                    "itemDescription" => sortDesc ? query.OrderByDescending(b=>b.ItemDescription) : query.OrderBy(b=>b.ItemDescription),
                    "itemBrand" => sortDesc ? query.OrderByDescending(b=>b.ItemBrand) : query.OrderBy(b=>b.ItemBrand),
                };
            }

            return await query.ToListAsync();
        }
    }
}
