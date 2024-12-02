using _00017159_WAD_Portfolio.Models;
using _00017159_WAD_Portfolio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _00017159_WAD_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllItems()
        {
            return await _repository.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (user.UserId == id)
            {
                await _repository.UpdateAsync(user);
                return NoContent();
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user
            )
        {
            await _repository.CreateAsync(user);
            return CreatedAtAction("GetByID", new { id = user.UserId }, user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
