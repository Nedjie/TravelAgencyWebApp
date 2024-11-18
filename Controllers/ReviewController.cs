using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Controllers
{
    public class ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
        : BaseController(logger)
    {
        private readonly IReviewService _reviewService = reviewService
            ?? throw new ArgumentNullException(nameof(reviewService));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

		[HttpGet("review/details/{id:int}")]
		public async Task<ActionResult<Review>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound(); // Return 404 if the review does not exist
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review cannot be null."); // Handle the case where review is null
            }

            await _reviewService.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();// 404 Not NFound
            }

            var existingReview = await _reviewService.GetReviewByIdAsync(id);

            if (existingReview == null)
            {
                return NotFound(); // Return 404 if the review does not exist
            }

            await _reviewService.UpdateReviewAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound(); // Return 404 if the review does not exist
            }

            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
