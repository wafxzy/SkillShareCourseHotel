using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions options) :base(options)
        {}
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(

                new Country
                {
                    Id = 1,
                    Name = "Ukraine",
                    ShortName = "Ukr"
                },
                new Country
                {
                    Id = 2,
                    Name = "Canada",
                    ShortName = "Can"
                },
                new Country
                {
                    Id = 3,
                    Name = "England",
                    ShortName = "Eng"
                }
                );
            builder.Entity<Hotel>().HasData(

               new Hotel
               {
                   Id = 1,
                   Name = "Kharkiv Hotel",
                   Adress = "Maydan Nezalezhnosti",
                   CountryId=1,
                   Rating=4.2
                   
               },
               new Hotel
               {
                   Id = 2,
                   Name = "SilverStone",
                   Adress = "New Scotland",
                   CountryId = 2,
                   Rating = 4.8
               },
               new Hotel
               {
                   Id = 3,
                   Name = "New faunland",
                   Adress = "London street 22b",
                   CountryId = 3,
                   Rating = 5.0
               }
               );
        }

    
    }
}
