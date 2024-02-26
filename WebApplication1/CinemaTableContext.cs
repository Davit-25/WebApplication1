using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Xml;
using WebApplication1.Areas.Identity.Data;
using WebApplication1.Congiguration;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1
{
    public class CinemaTableContext:DbContext
    {
        public DbSet<FilmsList> filmListDB { get; set; }
        public DbSet<Sessions> sessionsDB { get; set; }
        public DbSet<PackageOfCinema> PackageOfCinemaDB { get; set; }
       
        public CinemaTableContext()
        {

        }
        public CinemaTableContext(DbContextOptions<CinemaTableContext> options): base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sessions>()
                .Property(p => p.price)
                .HasColumnType("decimal(18,4)");
            modelBuilder.ApplyConfiguration(new FilmListConfig());
            modelBuilder.ApplyConfiguration(new SessionsConfig());
            modelBuilder.ApplyConfiguration(new PackOfCinConfig());
        }
    }
}
