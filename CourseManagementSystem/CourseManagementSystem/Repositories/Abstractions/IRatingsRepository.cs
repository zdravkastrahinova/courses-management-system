using CourseManagementSystem.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Repositories.Abstractions
{
    public interface IRatingsRepository
    {
        IQueryable<Rating> GetRatings();
        Rating GetById(int id);
        void Insert(Rating rating);
        void Update(Rating rating);
        void Delete(int id);
    }
}
