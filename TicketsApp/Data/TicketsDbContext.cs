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

        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Segment>()
                .HasIndex(s => new {s.TicketNumber, s.SerialNumber})
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}