using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Dtos;
namespace InventorySystem.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? search,
                                                     [FromQuery] string? sortBy,
                                                     [FromQuery] bool sortDesc)
        {
            var users = await _userService.GetAllUserAsync(search, sortBy, sortDesc);
            return Ok(users);
        }
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetItemByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = await _userService.CreateUser(createUserDto);
                return CreatedAtRoute("GetUserById", new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating a user: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updateUser = await _userService.UpdateUser(userId, dto);
                return UpdateUser == null ? NotFound() : Ok(updateUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating a user: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserAsync(userId);
                return isDeleted ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while deleting a user: {ex.Message}");
            }
        }
    }
}
