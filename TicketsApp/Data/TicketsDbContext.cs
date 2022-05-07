using Microsoft.EntityFrameworkCore;
using TicketsApp.Data.Models;

namespace TicketsApp.Data
{
    public class TicketsDbContext : DbContext
    {
        
        public DbSet<Segment> Segments { get; set;}

        public TicketsDbContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5433;Database=TicketsDb;Username=postgres;Password=sas949596");
        }
    }
}