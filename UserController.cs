using _00017159_WAD_Portfolio.Models;
using _00017159_WAD_Portfolio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _00017159_WAD_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Feedback> _feedbackRepository;

        public UserController(IRepository<User> userRepository, IRepository<Feedback> feedbackRepository)
        {
            _userRepository = userRepository;
            _feedbackRepository = feedbackRepository;
        }

        // Get all users
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        // Create new user
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            await _userRepository.CreateAsync(user);
            return CreatedAtAction("GetUserById", new { id = user.UserId }, user);
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user.UserId != id)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            await _userRepository.UpdateAsync(user);
            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // Optionally delete associated feedbacks if cascading isn't handled in the database
            var userFeedbacks = existingUser.feedbacks;
            if (userFeedbacks != null && userFeedbacks.Any())
            {
                foreach (var feedback in userFeedbacks)
                {
                    await _feedbackRepository.DeleteAsync(feedback.FeedbackId);
                }
            }

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }

        // Get feedbacks for a specific user
        [HttpGet("{id}/feedbacks")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetUserFeedbacks(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user.feedbacks);
        }
    }
}
