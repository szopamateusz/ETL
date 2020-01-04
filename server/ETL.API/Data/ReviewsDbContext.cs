using ETL.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ETL.API.Data
{
    public class ReviewsDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public ReviewsDbContext(DbContextOptions<ReviewsDbContext> options) : base(options)
        {
        }
    }
}
