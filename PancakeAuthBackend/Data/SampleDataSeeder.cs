using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;
using System.Security.Claims;

namespace PancakeAuthBackend.Data {
    public class SampleDataSeeder {
        private readonly BackendDataContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public SampleDataSeeder(BackendDataContext context, IServiceScope scope, IConfiguration config) {
            _context = context;
            _config = config;
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        }
        async public Task SeedAsync() {
            var password = _config.GetSection("Passwords").GetValue<string>("SuperAdmin");

            //address table

            var address1 = new Address {
                StreetName = "Wagner Street #22",
                City = "Hershey",
                Region = "Downtown",
                State = "Pennsylvania",
                PostalCode = "2456-82",
                Country = "USA"
            };

            var address2 = new Address {
                StreetName = "Browning Ave",
                City = "NYC",
                Region = "Bronx",
                State = "New York",
                PostalCode = "1155-82",
                Country = "USA"
            };

            var address3 = new Address {
                StreetName = "Turing Street, 35th Avenue",
                City = "Birmingham",
                Region = "Central England",
                State = "England",
                PostalCode = "S4356",
                Country = "UK"
            };
            _context.Addresses.Add(address1);
            _context.Addresses.Add(address2);
            _context.Addresses.Add(address3);

            //school table 

            var school1 = new School {
                Name = "Hershey",
                Address = address1,
            };

            var school2 = new School {
                Name = "Jefferson High",
                Address = address2,
            };

            var school3 = new School {
                Name = "Hawthrone Elementary",
                Address = address3,
            };

            _context.Schools.Add(school1);
            _context.Schools.Add(school2);
            _context.Schools.Add(school3);

            //billings table 

            var billing1 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 156200,
                Details = "Billed on 6 subscriptions. First Availed on the date 12/11/22",
                School = school1
            };
            var billing2 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 23900,
                Details = "Billed on 1 subscription(s). First Availed on the date 09/10/22",
                School = school1
            };
            var billing3 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 78650,
                Details = "Billed on 4 subscription(s). First Availed on the date 04/12/22",
                School = school2
            };
            var billing4 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 245790,
                Details = "Billed on 8 subscription(s). First Availed on the date 02/01/23",
                School = school3
            };
            var billing5 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 156200,
                Details = "Billed on 6 subscriptions. First Availed on the date 01/02/23",
                School = school3
            };

            _context.Payments.Add(billing1);
            _context.Payments.Add(billing2);
            _context.Payments.Add(billing3);
            _context.Payments.Add(billing4);
            _context.Payments.Add(billing5);

            //batches

            var batch1 = new Batch {
                Name = "5A",
                School = school1
            };

            var batch2 = new Batch {
                Name = "7A",
                School = school1
            };

            var batch3 = new Batch {
                Name = "2A",
                School = school2
            };

            var batch4 = new Batch {
                Name = "3A",
                School = school2
            };

            var batch5 = new Batch {
                Name = "5B",
                School = school3
            };

            var batch6 = new Batch {
                Name = "7B",
                School = school3
            };

            _context.Batches.Add(batch1);
            _context.Batches.Add(batch2);
            _context.Batches.Add(batch3);
            _context.Batches.Add(batch4);
            _context.Batches.Add(batch5);
            _context.Batches.Add(batch6);

            //grades

            var grade1 = new Grade {
                Name = "1"
            };

            var grade2 = new Grade {
                Name = "2"
            };

            var grade3 = new Grade {
                Name = "3"
            };

            var grade4 = new Grade {
                Name = "4"
            };

            var grade5 = new Grade {
                Name = "5"
            };

            var grade6 = new Grade {
                Name = "6"
            };

            var grade7 = new Grade {
                Name = "7"
            };

            var grade8 = new Grade {
                Name = "8"
            };

            var grade9 = new Grade {
                Name = "9"
            };

            var grade10 = new Grade {
                Name = "10"
            };

            var grade11 = new Grade {
                Name = "11"
            };

            var grade12 = new Grade {
                Name = "12"
            };

            _context.Grades.Add(grade1);
            _context.Grades.Add(grade2);
            _context.Grades.Add(grade3);
            _context.Grades.Add(grade4);
            _context.Grades.Add(grade5);
            _context.Grades.Add(grade6);
            _context.Grades.Add(grade7);
            _context.Grades.Add(grade8);
            _context.Grades.Add(grade9);
            _context.Grades.Add(grade10);
            _context.Grades.Add(grade11);
            _context.Grades.Add(grade12);

            //students - User
            var JonStewart = new User {
                UserName = "1HE234089",
                FirstName = "Jon",
                LastName = "Stewart",
                Email = "Jon@TonightShow.com",
                PhoneNumber = "555-7896",
            };
            await _userManager.CreateAsync(JonStewart, password);
            await _userManager.AddToRoleAsync(JonStewart, "Student");

            var MillieDyer = new User {
                UserName = "1JE768901",
                FirstName = "Millie",
                LastName = "Dyer",
                Email = "green@barrel.com",
                PhoneNumber = "555-2561",
            };
            await _userManager.CreateAsync(MillieDyer, password);
            await _userManager.AddToRoleAsync(MillieDyer, "Student");

            var CoreyBlack = new User {
                UserName = "1HT234586",
                FirstName = "Corey",
                LastName = "Black",
                Email = "Braazen@fox.com",
                PhoneNumber = "555-8576",
            };
            await _userManager.CreateAsync(CoreyBlack, password);
            await _userManager.AddToRoleAsync(CoreyBlack, "Student");

            var DanaWhite = new User {
                UserName = "1HE456456",
                FirstName = "Dana",
                LastName = "White",
                Email = "Dana@Verasity.com",
                PhoneNumber = "555-1111",
            };
            await _userManager.CreateAsync(DanaWhite, password);
            await _userManager.AddToRoleAsync(DanaWhite, "Student");

            var BlakeShelling = new User {
                UserName = "1HE093455",
                FirstName = "Blake",
                LastName = "Shelling",
                Email = "Power@Ranger.com",
                PhoneNumber = "555-7905",
            };
            await _userManager.CreateAsync(BlakeShelling, password);
            await _userManager.AddToRoleAsync(BlakeShelling, "Student");

            var NaomiWattson = new User {
                UserName = "1HE890123",
                FirstName = "Naomi",
                LastName = "Wattson",
                Email = "dark@detective.com",
                PhoneNumber = "555-5467"
            };
            await _userManager.CreateAsync(NaomiWattson, password);
            await _userManager.AddToRoleAsync(NaomiWattson, "Student");

            //students

            var student1 = new Student {
                StudentUID = "1HE234089",
                Name = "Jon Stewart",
                Email = "Jon@TonightShow.com",
                PhoneNumber = "555-7896",
                CityOfOrigin = "Orlando",
                StateOfOrigin = "Florida",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch1,
                Grade = grade5,
                School = school1,
                User = JonStewart
            };

            var student2 = new Student {
                StudentUID = "1JE768901",
                Name = "Millie Dyer",
                Email = "green@barrel.com",
                PhoneNumber = "555-2561",
                CityOfOrigin = "Detroit",
                StateOfOrigin = "Michigan",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch3,
                Grade = grade2,
                School = school2,
                User = MillieDyer
            };

            var student3 = new Student {
                StudentUID = "1HT234586",
                Name = "Corey Black",
                Email = "Braazen@fox.com",
                PhoneNumber = "555-8576",
                CityOfOrigin = "Jersey City",
                StateOfOrigin = "New Jersey",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch6,
                Grade = grade7,
                School = school3,
                User = CoreyBlack
            };

            var student4 = new Student {
                StudentUID = "1HE456456",
                Name = "Dana White",
                Email = "Dana@Verasity.com",
                PhoneNumber = "555-1111",
                CityOfOrigin = "Tuscon",
                StateOfOrigin = "Arizona",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch1,
                Grade = grade5,
                School = school1,
                User = DanaWhite
            };

            var student5 = new Student {
                StudentUID = "1HE093455",
                Name = "Blake Shelling",
                Email = "Power@Ranger.com",
                PhoneNumber = "555-7905",
                CityOfOrigin = "Miami",
                StateOfOrigin = "Florida",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch1,
                Grade = grade5,
                School = school1,
                User = BlakeShelling
            };

            var student6 = new Student {
                StudentUID = "1HE890123",
                Name = "Naomi Wattson",
                Email = "dark@detective.com",
                PhoneNumber = "555-5467",
                CityOfOrigin = "Seattle",
                StateOfOrigin = "Washington",
                CountryOfOrigin = "US",
                Nationality = "American",
                Batch = batch2,
                Grade = grade7,
                School = school1,
                User = NaomiWattson
            };

            _context.Students.Add(student1);
            _context.Students.Add(student2);
            _context.Students.Add(student3);
            _context.Students.Add(student4);
            _context.Students.Add(student5);
            _context.Students.Add(student6);

            //subjects

            var subject1 = new Subject {
                Name = "Physics"
            };

            var subject2 = new Subject {
                Name = "Math"
            };

            var subject3 = new Subject {
                Name = "Chemistry"
            };

            _context.Subjects.Add(subject1);
            _context.Subjects.Add(subject2);
            _context.Subjects.Add(subject3);

            //Subscriptions

            var subscription1 = new Subscription {
                Type = "Included",
                Name = "Default",
                Description = "Standard Subscription"
            };

            var subscription2 = new Subscription {
                Type = "Add-On",
                Name = "Math Magic",
                Description = "Added Modules for Math"
            };

            var subscription3 = new Subscription {
                Type = "Add-On",
                Name = "Physics Booster",
                Description = "Added Modules for Physics"
            };

            _context.Subscriptions.Add(subscription1);
            _context.Subscriptions.Add(subscription2);
            _context.Subscriptions.Add(subscription3);

            //Chapters

            var chapter1 = new Chapter {
                Title = "Gravity",
                Description = "Intro to Newtonian Gravitation",
                Subject = subject1
            };

            var chapter2 = new Chapter {
                Title = "Thermodynamics",
                Description = "Intro to Thermodynamics",
                Subject = subject1
            };

            var chapter3 = new Chapter {
                Title = "Kinematics",
                Description = "Intro to Kinematics",
                Subject = subject1
            };

            var chapter4 = new Chapter {
                Title = "Matrices",
                Description = "Intro to Matrix Manipulation",
                Subject = subject2
            };

            var chapter5 = new Chapter {
                Title = "Vectors",
                Description = "Intro to 2D Vectors",
                Subject = subject2
            };

            var chapter6 = new Chapter {
                Title = "Organic Chemistry",
                Description = "Intro to Organic Chemistry",
                Subject = subject3
            };

            _context.Chapters.Add(chapter1);
            _context.Chapters.Add(chapter2);
            _context.Chapters.Add(chapter3);
            _context.Chapters.Add(chapter4);
            _context.Chapters.Add(chapter5);
            _context.Chapters.Add(chapter6);

            //Add Chapters to Subscriptions

            var chaptersIncluded1 = new ChaptersIncluded {
                Subscription = subscription1,
                Chapter = chapter1
            };

            var chaptersIncluded2 = new ChaptersIncluded {
                Subscription = subscription1,
                Chapter = chapter4
            };

            var chaptersIncluded3 = new ChaptersIncluded {
                Subscription = subscription1,
                Chapter = chapter6
            };

            var chaptersIncluded4 = new ChaptersIncluded {
                Subscription = subscription2,
                Chapter = chapter4
            };

            var chaptersIncluded5 = new ChaptersIncluded {
                Subscription = subscription2,
                Chapter = chapter5
            };

            var chaptersIncluded6 = new ChaptersIncluded {
                Subscription = subscription3,
                Chapter = chapter1
            };

            var chaptersIncluded7 = new ChaptersIncluded {
                Subscription = subscription3,
                Chapter = chapter2
            };

            var chaptersIncluded8 = new ChaptersIncluded {
                Subscription = subscription3,
                Chapter = chapter3
            };

            _context.ChaptersIncluded.Add(chaptersIncluded1);
            _context.ChaptersIncluded.Add(chaptersIncluded2);
            _context.ChaptersIncluded.Add(chaptersIncluded3);
            _context.ChaptersIncluded.Add(chaptersIncluded4);
            _context.ChaptersIncluded.Add(chaptersIncluded5);
            _context.ChaptersIncluded.Add(chaptersIncluded6);
            _context.ChaptersIncluded.Add(chaptersIncluded7);
            _context.ChaptersIncluded.Add(chaptersIncluded8);

            //Add subscriptions to school

            var availedSubscription1 = new AvailedSubscription {
                School = school1,
                Subscription = subscription1
            };

            var availedSubscription2 = new AvailedSubscription {
                School = school1,
                Subscription = subscription2
            };

            var availedSubscription3 = new AvailedSubscription {
                School = school1,
                Subscription = subscription3
            };

            var availedSubscription4 = new AvailedSubscription {
                School = school2,
                Subscription = subscription1
            };

            var availedSubscription5 = new AvailedSubscription {
                School = school2,
                Subscription = subscription2
            };

            var availedSubscription6 = new AvailedSubscription {
                School = school3,
                Subscription = subscription1
            };

            _context.AvailedSubscription.Add(availedSubscription1);
            _context.AvailedSubscription.Add(availedSubscription2);
            _context.AvailedSubscription.Add(availedSubscription3);
            _context.AvailedSubscription.Add(availedSubscription4);
            _context.AvailedSubscription.Add(availedSubscription5);
            _context.AvailedSubscription.Add(availedSubscription6);

            await _context.SaveChangesAsync();
        }
    }
}
