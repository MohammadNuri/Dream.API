using Dream.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dream.API.DbContext

{
    public class DreamApiDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public DreamApiDbContext(DbContextOptions<DreamApiDbContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        //u can use these instead  of use the configuration in program.cs 
        //but in this project we use it in DiContainer 
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
