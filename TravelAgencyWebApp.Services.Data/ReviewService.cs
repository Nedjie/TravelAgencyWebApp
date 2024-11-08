﻿using TravelAgencyWebApp.Common.ErrorMessages;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review,int> _reviewRepository;

        public ReviewService(IRepository<Review, int> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task AddReviewAsync(Review review)
        {
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                throw new EntityNotFoundException($"Review with ID {id} not found.");
            }

            await _reviewRepository.DeleteAsync(review);
        }

    }

}