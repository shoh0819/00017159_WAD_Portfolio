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
        public FeedbackController(IRepository<Feedback> repository)
        {
            _repository = repository;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Feedback/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedbackById(int id)
        {
            var feedback = await _repository.GetByIdAsync(id);
            return feedback == null ? NotFound() : Ok(feedback);
        }

        // PUT: api/Feedback/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, Feedback feedback)
        {
            if (feedback.FeedbackId == id)
            {
                await _repository.UpdateAsync(feedback);
                return NoContent();
            }
            else
                return BadRequest();
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult> CreateFeedback(Feedback feedback)
        {
            await _repository.CreateAsync(feedback);
            return CreatedAtAction("GetFeedbackById", new { id = feedback.FeedbackId }, feedback);
        }

        // DELETE: api/Feedback/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
