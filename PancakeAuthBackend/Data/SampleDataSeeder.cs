using Microsoft.EntityFrameworkCore;

namespace PancakeAuthBackend.Data {
    public static class SampleDataSeeder {

        public static void Seed(ModelBuilder modelBuilder) {

            //address table
            modelBuilder.Entity<Address>().HasData(
                new Address {
                    Id = 1,
                    StreetName = "Wagner Street #22",
                    City = "Hershey",
                    Region = "Downtown",
                    State = "Pennsylvania",
                    PostalCode = "2456-82",
                    Country = "USA"
                }
            );

            //school table 
            modelBuilder.Entity<School>().HasData(
               new School {
                   Id = 1,
                   Name = "Hershey",
                   AddressId = 1,
               }
           );
        }
    }
}
