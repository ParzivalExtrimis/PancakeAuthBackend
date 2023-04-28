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
                },

                 new Address {
                     Id = 2,
                     StreetName = "Browning Ave",
                     City = "NYC",
                     Region = "Bronx",
                     State = "New York",
                     PostalCode = "1155-82",
                     Country = "USA"
                 },

                  new Address {
                      Id = 3,
                      StreetName = "Turing Street, 35th Avenue",
                      City = "Birmingham",
                      Region = "Central England",
                      State = "England",
                      PostalCode = "S4356",
                      Country = "UK"
                  }
            );

            //school table 
            modelBuilder.Entity<School>().HasData(
               new School {
                   Id = 1,
                   Name = "Hershey",
                   AddressId = 1,
               },

                new School {
                    Id = 2,
                    Name = "Jefferson High",
                    AddressId = 2,
                },

                 new School {
                     Id = 3,
                     Name = "Hawthrone Elementary",
                     AddressId = 3,
                 }
           );

            //billings table 
            modelBuilder.Entity<Billing>().HasData(
               new Billing {
                   Id = 1,
                   Status = "Pending",
                   DueDate = DateTime.Now,
                   Amount = 156200,
                   Details = "Billed on 6 subscriptions. First Availed on the date 12/11/22",
                   SchoolId = 1
               },
                new Billing {
                    Id = 2,
                    Status = "Completed",
                    DueDate = DateTime.Now,
                    Amount = 23900,
                    Details = "Billed on 1 subscription(s). First Availed on the date 09/10/22",
                    SchoolId = 1
                },
                 new Billing {
                     Id = 3,
                     Status = "Pending",
                     DueDate = DateTime.Now,
                     Amount = 78650,
                     Details = "Billed on 4 subscription(s). First Availed on the date 04/12/22",
                     SchoolId = 2
                 },
                  new Billing {
                      Id = 4,
                      Status = "Pending",
                      DueDate = DateTime.Now,
                      Amount = 245790,
                      Details = "Billed on 8 subscription(s). First Availed on the date 02/01/23",
                      SchoolId = 3
                  },
                   new Billing {
                       Id = 5,
                       Status = "Completed",
                       DueDate = DateTime.Now,
                       Amount = 156200,
                       Details = "Billed on 6 subscriptions. First Availed on the date 01/02/23",
                       SchoolId = 3
                   }
           );

            //batches
            modelBuilder.Entity<Batch>().HasData(
                
                    new Batch {
                        Id = 1,
                        Name = "5A",
                        SchoolId = 1
                    },

                    new Batch {
                        Id = 2,
                        Name = "7A",
                        SchoolId = 1
                    },

                    new Batch {
                        Id = 3,
                        Name = "2A",
                        SchoolId = 2
                    },

                    new Batch {
                        Id = 4,
                        Name = "3A",
                        SchoolId = 2
                    },

                    new Batch {
                        Id = 5,
                        Name = "5B",
                        SchoolId = 3
                    },

                    new Batch {
                        Id = 6,
                        Name = "7B",
                        SchoolId = 3
                    }


            );
            //grades
            modelBuilder.Entity<Grade>().HasData(

                    new Grade {
                        Id = 1,
                        Name = "1"
                    },

                    new Grade {
                        Id = 2,
                        Name = "2"
                    },

                    new Grade {
                        Id = 3,
                        Name = "3"
                    },

                    new Grade {
                        Id = 4,
                        Name = "4"
                    },

                    new Grade {
                        Id = 5,
                        Name = "5"
                    },

                    new Grade {
                        Id = 6,
                        Name = "6"
                    },

                    new Grade {
                        Id = 7,
                        Name = "7"
                    },

                    new Grade {
                        Id = 8,
                        Name = "8"
                    },

                    new Grade {
                        Id = 9,
                        Name = "9"
                    },

                    new Grade {
                        Id = 10,
                        Name = "10"
                    },

                    new Grade {
                        Id = 11,
                        Name = "11"
                    },

                    new Grade {
                        Id = 12,
                        Name = "12"
                    }
            );

            //students
            modelBuilder.Entity<Student>().HasData(

                  new Student {
                      Id = 1,
                      StudentUID = "1HE234089",
                      Name = "Jon Stewart",
                      Email = "Jon@TonightShow.com",
                      PhoneNumber = "555-7896",
                      CityOfOrigin = "Orlando",
                      StateOfOrigin = "Florida",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 1,
                      GradeId = 5,
                      SchoolId = 1
                  },

                  new Student {
                      Id = 2,
                      StudentUID = "1JE768901",
                      Name = "Millie Dyer",
                      Email = "green@barrel.com",
                      PhoneNumber = "555-2561",
                      CityOfOrigin = "Detroit",
                      StateOfOrigin = "Michigan",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 3,
                      GradeId = 2,
                      SchoolId = 2
                  },

                  new Student {
                      Id = 3,
                      StudentUID = "1HT234586",
                      Name = "Corey Black",
                      Email = "Braazen@fox.com",
                      PhoneNumber = "555-8576",
                      CityOfOrigin = "Jersey City",
                      StateOfOrigin = "New Jersey",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 6,
                      GradeId = 7,
                      SchoolId = 3
                  },

                  new Student {
                      Id = 4,
                      StudentUID = "1HE456456",
                      Name = "Dana White",
                      Email = "Dana@Verasity.com",
                      PhoneNumber = "555-1111",
                      CityOfOrigin = "Tuscon",
                      StateOfOrigin = "Arizona",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 1,
                      GradeId = 5,
                      SchoolId = 1
                  },

                  new Student {
                      Id = 5,
                      StudentUID = "1HE093455",
                      Name = "Blake Shelling",
                      Email = "Power@Ranger.com",
                      PhoneNumber = "555-7905",
                      CityOfOrigin = "Miami",
                      StateOfOrigin = "Florida",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 1,
                      GradeId = 5,
                      SchoolId = 1
                  },

                  new Student {
                      Id = 6,
                      StudentUID = "1HE890123",
                      Name = "Naomi Wattson",
                      Email = "dark@detective.com",
                      PhoneNumber = "555-5467",
                      CityOfOrigin = "Seattle",
                      StateOfOrigin = "Washington",
                      CountryOfOrigin = "US",
                      Nationality = "American",
                      BatchId = 2,
                      GradeId = 7,
                      SchoolId = 1
                  }
              );
            
                //subjects
              modelBuilder.Entity<Subject>().HasData(

                  new Subject {
                      Id = 1,
                      Name = "Physics"
                  },

                  new Subject {
                      Id = 2,
                      Name = "Math"
                  },

                  new Subject {
                      Id = 3,
                      Name = "Chemistry"
                  }
              );

            //Subscriptions
            modelBuilder.Entity<Subscription>().HasData(
            
                  new Subscription {
                      Id = 1,
                      Type = "Included",
                      Name = "Default",
                      Description = "Standard Subscription"
                  },

                  new Subscription {
                      Id = 2,
                      Type = "Add-On",
                      Name = "Math Magic",
                      Description = "Added Modules for Math"
                  },

                  new Subscription {
                      Id = 3,
                      Type = "Add-On",
                      Name = "Physics Booster",
                      Description = "Added Modules for Physics"
                  }
            );

            //Chapters
            modelBuilder.Entity<Chapter>().HasData(

                  new Chapter {
                      Id = 1,
                      Title = "Gravity",
                      Description = "Intro to Newtonian Gravitation",
                      SubjectId = 1
                  },

                  new Chapter {
                      Id = 2,
                      Title = "Thermodynamics",
                      Description = "Intro to Thermodynamics",
                      SubjectId = 1
                  },

                  new Chapter {
                      Id = 3,
                      Title = "Kinematics",
                      Description = "Intro to Kinematics",
                      SubjectId = 1
                  },

                  new Chapter {
                      Id = 4,
                      Title = "Matrices",
                      Description = "Intro to Matrix Manipulation",
                      SubjectId = 2
                  },

                  new Chapter {
                      Id = 5,
                      Title = "Vectors",
                      Description = "Intro to 2D Vectors",
                      SubjectId = 2
                  },

                  new Chapter {
                      Id = 6,
                      Title = "Organic Chemistry",
                      Description = "Intro to Organic Chemistry",
                      SubjectId = 3
                  }
            );
            //Add Chapters to Subscriptions
            modelBuilder.Entity<ChaptersIncluded>().HasData(

                   new ChaptersIncluded {
                       SubscriptionId = 1,
                       ChapterId = 1
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 1,
                       ChapterId = 4
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 1,
                       ChapterId = 6
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 2,
                       ChapterId = 4
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 2,
                       ChapterId = 5
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 3,
                       ChapterId = 1
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 3,
                       ChapterId = 2
                   },

                   new ChaptersIncluded {
                       SubscriptionId = 3,
                       ChapterId = 3
                   }
             );

            //Add subscriptions to school
            modelBuilder.Entity<AvailedSubscription>().HasData(
                    
                   new AvailedSubscription {
                       SchoolId = 1,
                       SubscriptionId = 1
                   },

                   new AvailedSubscription {
                       SchoolId = 1,
                       SubscriptionId = 2
                   },

                   new AvailedSubscription {
                       SchoolId = 1,
                       SubscriptionId = 3
                   },

                   new AvailedSubscription {
                       SchoolId = 2,
                       SubscriptionId = 1
                   },

                   new AvailedSubscription {
                       SchoolId = 2,
                       SubscriptionId = 2
                   },

                   new AvailedSubscription {
                       SchoolId = 3,
                       SubscriptionId = 1
                   }
            );
        }
    }
}
