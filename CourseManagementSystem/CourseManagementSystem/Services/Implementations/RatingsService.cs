using CourseManagementSystem.Entities;
using CourseManagementSystem.Models.Ratings;
using CourseManagementSystem.Repositories.Abstractions;
using CourseManagementSystem.Services.Abstractions;
using System.Linq;

namespace CourseManagementSystem.Services.Implementations
{
    public class RatingsService : IRatingsService
    {
        private readonly IRatingsRepository repository;

        public RatingsService(IRatingsRepository repository)
        {
            this.repository = repository;
        }

        public void Insert(RatingEditViewModel model)
        {
            Rating rating = new Rating
            {
                CourseId = model.CourseId,
                Rate = model.Rate
            };

            repository.Insert(rating);
        }

        public double CalculateRating(int courseId)
        {
            return repository
                .GetRatings()
                .Where(rating => rating.CourseId == courseId)
                .Select(rating => rating.Rate)
                .Average();
        }
    }
}
