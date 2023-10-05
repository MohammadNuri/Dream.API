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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("Tehran")
                    {
                        Id = 1,
                        Description = "Capital City!",
                    },
                    new City("Shiraz")
                    {
                        Id = 2,
                        Description = "a Large City",
                    },
                    new City("Esfahan")
                    {
                        Id = 3,
                        Description = "Half of the World :))",
                    },
                    new City("Mashhad")
                    {
                        Id = 4,
                        Description = "a Shit one...",
                    }
                    );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Point Of Interest Tehran")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "Point Of Interest Tehran",
                    },
                    new PointOfInterest("Point Of Interest Shiraz")
                    {
                        Id = 2,
                        CityId = 2,
                        Description = "Point Of Interest Shiraz",
                    },
                    new PointOfInterest("Point Of Interest Esfahan")
                    {
                        Id = 3,
                        CityId = 3,
                        Description = "Point Of Interest Esfahan",
                    },
                    new PointOfInterest("Point Of Interest Mashhad")
                    {
                        Id = 4,
                        CityId = 4,
                        Description = "Point Of Interest Mashhad",
                    }
                    );

            base.OnModelCreating(modelBuilder);
        }
    }
}
