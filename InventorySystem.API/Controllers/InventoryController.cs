using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Core.Entities;
using InventorySystem.Core.Dtos;
using InventorySystem.Core.Interfaces;
namespace InventorySystem.API.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IItemService _itemService;
        public InventoryController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory([FromQuery] string? search,
                                                         [FromQuery] string? sortBy,
                                                         [FromQuery] bool sortDesc)
        {
            var items = await _itemService.GetAllItemsAsync(search, sortBy, sortDesc);
            return Ok(items);
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public async Task<IActionResult> GetItemByIdAsync(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemAsync([FromBody] CreateItemDto createItemDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var item = await _itemService.CreateItemAsync(createItemDto);
                return CreatedAtRoute("GetItemById", new { id = item.ItemId }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating an inventory: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, UpdateItemDto updateItemDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updateItem = await _itemService.UpdateItemAsync(id, updateItemDto);
                return updateItem == null ? NotFound() : Ok(updateItem);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured while updating the inventory: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(int id)
        {
            try
            {
                var isDeleted = await _itemService.DeleteItemAsync(id);
                return isDeleted ? NoContent() : NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured while deleting the inventory: {ex.Message}");
            }
        }
    }
}
