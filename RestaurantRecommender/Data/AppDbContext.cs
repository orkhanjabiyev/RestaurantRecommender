using Microsoft.EntityFrameworkCore;
using RestaurantRecommender.Models;

namespace RestaurantRecommender.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
