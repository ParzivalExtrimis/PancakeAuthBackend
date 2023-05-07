using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace PancakeAuthBackend.Services {
    public class SchoolService : ISchoolService {
        readonly BackendDataContext _context;
        readonly UserManager<User> _user;
        public SchoolService(BackendDataContext context, UserManager<User> userManager) {
            _context = context;
            _user = userManager;
        }

        //util
        async private Task<School?> InitSchool(string name) {
            return await _context.Schools.SingleOrDefaultAsync(school => school.Name == name);
        }


        //********************************************************************************

        //in-service utility methods
        async public Task<bool> SchoolExists(string name)
            => await _context.Schools.AnyAsync(school => school.Name == name);
        

        //********************************************************************************

        //service methods
        async public Task<SchoolDTO?> GetSchoolData(string name) {
            return await _context.Schools
                .Include(sc => sc.Address)
                .Where(s => s.Name == name)
                .Select( school =>
                    new SchoolDTO {
                        Name = school.Name,
                        StreetName = school.Address.StreetName,
                        City = school.Address.City,
                        Region = school.Address.Region,
                        State = school.Address.State,
                        PostalCode = school.Address.PostalCode,
                        Country = school.Address.Country
                    }
                )
                .FirstOrDefaultAsync();
        }

        async public Task<List<StudentDTO>> GetSchoolStudents(string schoolName) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Department)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName,
                        LastName = s.User.LastName,
                        ProfilePicture = s.User.ProfilePicture,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Department = s.Department.Name,
                        School = s.School.Name
                    })
                    .ToListAsync();
            }

            return studentObjects;
        } 

        async public Task<List<StudentDTO>> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Department)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName,
                        LastName = s.User.LastName,
                        ProfilePicture = s.User.ProfilePicture,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Department = s.Department.Name,
                        School = s.School.Name
                    })
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByDepartment(string schoolName, string department) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Department)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school && student.Department.Name == department)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName,
                        LastName = s.User.LastName,
                        ProfilePicture = s.User.ProfilePicture,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Department = s.Department.Name,
                        School = s.School.Name
                    })
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByDepartmentPaged(string schoolName, string department, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Department)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school && student.Department.Name == department)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName,
                        LastName = s.User.LastName,
                        ProfilePicture = s.User.ProfilePicture,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Department = s.Department.Name,
                        School = s.School.Name
                    })
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<bool> ISchoolService.AddStudents(string schoolName, List<StudentDTO> studentObjects) {
            var school = await InitSchool(schoolName);
            var studentList = new List<Student>();
            foreach (var studentObj in studentObjects) {
               
                var sDepartment = await _context.Departments
                    .FirstOrDefaultAsync(grade => grade.Name == studentObj.Department);
               
                if (sDepartment == null || school == null) {
                    return false;
                }

                var studentUser = new User {
                    UserName = studentObj.StudentUID,
                    FirstName = studentObj.FirstName,
                    LastName = studentObj.LastName,
                    ProfilePicture = studentObj.ProfilePicture,
                    Email = studentObj.Email,
                    PhoneNumber = studentObj.PhoneNumber,
                };
                await _user.CreateAsync(studentUser, studentObj.Password);
                await _user.AddToRoleAsync(studentUser, "Student");


                // Add to Student Contexted object and make a list of them
                var s = new Student {
                    StudentUID = studentObj.StudentUID,
                    Name = studentObj.Name,
                    Email = studentObj.Email,
                    PhoneNumber = studentObj.PhoneNumber,
                    CityOfOrigin = studentObj.CityOfOrigin,
                    StateOfOrigin = studentObj.StateOfOrigin,
                    CountryOfOrigin = studentObj.CountryOfOrigin,
                    Nationality = studentObj.Nationality,
                    Department = sDepartment,
                    User = studentUser,
                    School = school
                };
                studentList.Add(s);

            }
            await _context.Students.AddRangeAsync(studentList);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.EditStudent(StudentDTO studentObj, string SSID, string schoolName) {
            var s = await _context.Students
                .Include(s => s.School)
                .FirstOrDefaultAsync(s => 
                   s.StudentUID == SSID
                && s.School.Name == schoolName);

            if(s is null) { 
                return false;
            }

            //make new student from DTO

            var sDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.Name == studentObj.Department);

            var sSchool = await _context.Schools
                .FirstOrDefaultAsync(s => s.Name == studentObj.School);

            if (sDepartment is null || sSchool is null) {
                return false;
            }

            var user = await _user.FindByNameAsync(studentObj.StudentUID);
            user.FirstName = studentObj.FirstName;
            user.LastName = studentObj.LastName;
            user.Email = studentObj.Email;
            user.PhoneNumber = studentObj.PhoneNumber;
            user.ProfilePicture = studentObj.ProfilePicture;
            user.UserName = studentObj.StudentUID;

            var student = new Student {
                StudentUID = studentObj.StudentUID,
                Name = studentObj.Name,
                Email = studentObj.Email,
                PhoneNumber = studentObj.PhoneNumber,
                CityOfOrigin = studentObj.CityOfOrigin,
                StateOfOrigin = studentObj.StateOfOrigin,
                CountryOfOrigin = studentObj.CountryOfOrigin,
                Nationality = studentObj.Nationality,
                Department = sDepartment,
                User = user
            };

            //replace student from db
            _context.Remove(s);
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.DeleteStudent(string SUID, string schoolName) {
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(student =>  student.StudentUID == SUID);
            
            if (student == null) {
                return false;
            }
            await _user.DeleteAsync(student.User);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
