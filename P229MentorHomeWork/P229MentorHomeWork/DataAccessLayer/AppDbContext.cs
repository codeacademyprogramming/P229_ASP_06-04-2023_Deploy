using Microsoft.EntityFrameworkCore;
using P229MentorHomeWork.Models;

namespace P229MentorHomeWork.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<PricingOffer> PricingOffers { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}
