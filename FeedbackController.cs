using _00017159_WAD_Portfolio.Models;
using _00017159_WAD_Portfolio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _00017159_WAD_Portfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IRepository<Feedback> _repository;
        private readonly IRepository<User> _userRepository;

        public FeedbackController(IRepository<Feedback> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        // Get all feedbacks
        [HttpGet]
        public async Task<IEnumerable<Feedback>> GetAllItems()
        {
            return await _repository.GetAllAsync();
        }

        // Get feedback by ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            return feedback == null ? NotFound() : Ok(feedback);
        }

        // Create new feedback
        [HttpPost]
        public async Task<ActionResult> Create(Feedback feedback)
        {
            // Validate the UserId (foreign key)
            var userExists = await _userRepository.GetByIdAsync(feedback.UserId);
            if (userExists == null)
            {
                return BadRequest($"User with ID {feedback.UserId} does not exist.");
            }

            await _repository.CreateAsync(feedback);
            return CreatedAtAction("GetById", new { id = feedback.FeedbackId }, feedback);
        }

        // Update feedback
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Feedback feedback)
        {
            if (feedback.FeedbackId != id)
            {
                return BadRequest("Feedback ID mismatch.");
            }

            // Validate the UserId (foreign key)
            var userExists = await _userRepository.GetByIdAsync(feedback.UserId);
            if (userExists == null)
            {
                return BadRequest($"User with ID {feedback.UserId} does not exist.");
            }

            var existingFeedback = await _repository.GetByIdAsync(id);
            if (existingFeedback == null)
            {
                return NotFound($"Feedback with ID {id} not found.");
            }

            await _repository.UpdateAsync(feedback);
            return NoContent();
        }

        // Delete feedback
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingFeedback = await _repository.GetByIdAsync(id);
            if (existingFeedback == null)
            {
                return NotFound($"Feedback with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
