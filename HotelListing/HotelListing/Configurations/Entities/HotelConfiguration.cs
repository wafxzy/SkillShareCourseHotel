using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(

           new Hotel
           {
               Id = 1,
               Name = "Kharkiv Hotel",
               Adress = "Maydan Nezalezhnosti",
               CountryId = 1,
               Rating = 4.2

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
