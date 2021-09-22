using CourseManagementSystem.Entities;
using CourseManagementSystem.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseManagementSystem.Repositories.Implementations
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly CoursesManagementSystemContext context;
        private readonly DbSet<Rating> dbSet;

        public RatingsRepository(CoursesManagementSystemContext context)
        {
            this.context = context;
            dbSet = context.Set<Rating>();
        }

        public IQueryable<Rating> GetRatings()
        {
            return dbSet;
        }

        public Rating GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Rating rating)
        {
            dbSet.Add(rating);
            context.SaveChanges();
        }

        public void Update(Rating rating)
        {
            Rating element = GetById(rating.Id);

            if (element != null)
            {
                context.Entry<Rating>(element).State = EntityState.Detached;
            }

            context.Entry<Rating>(rating).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            dbSet.Remove(GetById(id));
            context.SaveChanges();
        }
    }
}
