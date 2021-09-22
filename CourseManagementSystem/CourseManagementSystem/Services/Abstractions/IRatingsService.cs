using CourseManagementSystem.Models.Ratings;

namespace CourseManagementSystem.Services.Abstractions
{
    public interface IRatingsService
    {
        void Insert(RatingEditViewModel model);

        double CalculateRating(int courseId);
    }
}
