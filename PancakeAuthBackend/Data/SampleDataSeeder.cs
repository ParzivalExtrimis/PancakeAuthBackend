using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;
using System.Security.Claims;

namespace PancakeAuthBackend.Data {
    public class SampleDataSeeder : ISampleDataSeeder {
        private readonly BackendDataContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public SampleDataSeeder(BackendDataContext context, UserManager<User> userManager, IConfiguration config) {
            _context = context;
            _config = config;
            _userManager = userManager;
        }
        async Task<int> ISampleDataSeeder.SeedAsync() {
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

            //departments
            var Oncology = new Department {
                Name = "Oncology"
            };

            var Opthamology = new Department {
                Name = "Opthamology"
            };

            var Cardiology = new Department {
                Name = "Cardiology"
            };

            var Pathology = new Department {
                Name = "Pathology"
            };

            var Radiology = new Department {
                Name = "Radiology"
            };

            var Pediatrics = new Department {
                Name = "Pediatrics"
            };

            var Orthopedics = new Department {
                Name = "Orthopedics"
            };

            var Dermatology = new Department {
                Name = "Dermatology"
            };

            await _context.Departments.AddAsync(Oncology);
            await _context.Departments.AddAsync(Opthamology);
            await _context.Departments.AddAsync(Cardiology);
            await _context.Departments.AddAsync(Pathology);
            await _context.Departments.AddAsync(Radiology);
            await _context.Departments.AddAsync(Pediatrics);
            await _context.Departments.AddAsync(Orthopedics);
            await _context.Departments.AddAsync(Dermatology);

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
                Department = Oncology,
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
                Department = Opthamology,
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
                Department = Radiology,
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
                Department = Dermatology,
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
                Department = Pathology,
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
                Department = Oncology,
                School = school1,
                User = NaomiWattson
            };

            _context.Students.Add(student1);
            _context.Students.Add(student2);
            _context.Students.Add(student3);
            _context.Students.Add(student4);
            _context.Students.Add(student5);
            _context.Students.Add(student6);

            //billings table 

            var billing1 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 156200,
                Details = "Billed on 6 subscriptions. First Availed on the date 12/11/22",
                Student = student1
            };
            var billing2 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 23900,
                Details = "Billed on 1 subscription(s). First Availed on the date 09/10/22",
                Student = student1
            };
            var billing3 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 78650,
                Details = "Billed on 4 subscription(s). First Availed on the date 04/12/22",
                Student = student2
            };
            var billing4 = new Billing {
                Status = "Pending",
                DueDate = DateTime.Now,
                Amount = 245790,
                Details = "Billed on 8 subscription(s). First Availed on the date 02/01/23",
                Student = student3
            };
            var billing5 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 156200,
                Details = "Billed on 6 subscriptions. First Availed on the date 01/02/23",
                Student = student3
            };
            var billing6 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 12300,
                Details = "Billed on 5 subscriptions. First Availed on the date 01/02/23",
                Student = student4
            };
            var billing7 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 28900,
                Details = "Billed on 11 subscriptions. First Availed on the date 01/02/23",
                Student = student5
            };
            var billing8 = new Billing {
                Status = "Completed",
                DueDate = DateTime.Now,
                Amount = 9870,
                Details = "Billed on 1 subscriptions. First Availed on the date 01/02/23",
                Student = student6
            };

            _context.Payments.Add(billing1);
            _context.Payments.Add(billing2);
            _context.Payments.Add(billing3);
            _context.Payments.Add(billing4);
            _context.Payments.Add(billing5);
            _context.Payments.Add(billing6);
            _context.Payments.Add(billing7);
            _context.Payments.Add(billing8);

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

            return await _context.SaveChangesAsync();
        }
    }
}
