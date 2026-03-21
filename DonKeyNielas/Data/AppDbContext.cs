using Microsoft.EntityFrameworkCore;
using DonKeyNielas.Entities;

namespace DonKeyNielas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets (tablas)
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Quiniela> Quinielas => Set<Quiniela>();
        public DbSet<QuinielaForecast> QuinielasForecasts => Set<QuinielaForecast>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Championship> Championships => Set<Championship>();
        public DbSet<InviteCode> InviteCodes => Set<InviteCode>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
    
}