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
                .Include(sc => sc.Subscriptions)
                .Include(sc => sc.Address)
                .Where(s => s.Name == name)
                .Select( school =>
                    new SchoolDTO {
                        Name = school.Name,
                        Subscriptions = school.Subscriptions != null 
                                ? school.Subscriptions.Select(s => s.Name).ToList()
                                : new List<string>(),
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
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
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
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
                    })
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByGrade(string schoolName, string grade) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school && student.Grade.Name == grade)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
                    })
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByGradePaged(string schoolName, string grade, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(student => student.School == school && student.Grade.Name == grade)
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Name = s.Name,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
                    })
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByBatch(string schoolName, string batch) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(
                        student => student.School == school 
                     && student.Batch != null 
                     && student.Batch.Name == batch
                    )
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
                    })
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByBatchPaged(string schoolName, string batch, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                studentObjects = await _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.Grade)
                    .Include(s => s.School)
                    .Include(s => s.User)
                    .Where(
                        student => student.School == school
                     && student.Batch != null
                     && student.Batch.Name == batch
                    )
                    .Select(s => new StudentDTO {
                        StudentUID = s.StudentUID,
                        Name = s.Name,
                        FirstName = s.User.FirstName ?? "",
                        LastName = s.User.LastName ?? "",
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        CityOfOrigin = s.CityOfOrigin,
                        StateOfOrigin = s.StateOfOrigin,
                        CountryOfOrigin = s.CountryOfOrigin,
                        Nationality = s.Nationality,
                        Batch = s.Batch != null ? s.Batch.Name : "None",
                        School = s.School.Name,
                        Grade = s.Grade.Name
                    })
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return studentObjects;
        }

        async Task<List<SubscriptionDTO>> ISchoolService.GetSchoolSubscriptions(string schoolName) {
            var school = InitSchool(schoolName);
            var subscriptionObjects = new List<SubscriptionDTO>();
            if (school != null) {
                subscriptionObjects = await _context.Subscriptions
                    .Include(s => s.AvailedSchools)
                    .Where(s => s.AvailedSchools
                        .Any(a => a.SchoolId == school.Id))
                    .Select(subscription => new SubscriptionDTO {
                        Type = subscription.Type,
                        Name = subscription.Name,
                        Description = subscription.Description,
                        IncludedChapters = subscription.Chapters.Select(c => c.Title).ToList(),
                    })
                    .ToListAsync();
            }
            return subscriptionObjects;
        }

        async Task<List<BillingDTO>> ISchoolService.GetSchoolBillings(string schoolName) {
            var school = await InitSchool(schoolName);
            var payments = new List<BillingDTO>();
            var pays = await _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .ToListAsync();

            foreach(var pay in pays) {
                var payment = new BillingDTO {
                    status = pay.Status,
                    dueDate = pay.DueDate,
                    Amount = pay.Amount,
                    Details = pay.Details,
                    School = school!.Name
                }; 
            }
            return payments;
        }

        async Task<List<BillingDTO>> ISchoolService.GetSchoolBillingsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var payments = new List<BillingDTO>();
            var pays = await _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            foreach (var pay in pays) {
                var payment = new BillingDTO {
                    status = pay.Status,
                    dueDate = pay.DueDate,
                    Amount = pay.Amount,
                    Details = pay.Details,
                    School = school!.Name
                };
            }
            return payments;
        }

        async Task<bool> ISchoolService.AddStudents(string schoolName, List<StudentDTO> studentObjects) {
            var school = await InitSchool(schoolName);
            var studentList = new List<Student>();
            foreach (var studentObj in studentObjects) {
                var SBatch = await _context.Batches
                    .FirstOrDefaultAsync(batch => studentObj.Batch != null && batch.Name == studentObj.Batch);

                var SGrade = await _context.Grades
                    .FirstOrDefaultAsync(grade => grade.Name == studentObj.Grade);
               
                if (SGrade == null || school == null) {
                    return false;
                }

                var studentUser = new User {
                    UserName = studentObj.StudentUID,
                    FirstName = studentObj.FirstName,
                    LastName = studentObj.LastName,
                    Email = studentObj.Email,
                    PhoneNumber = studentObj.PhoneNumber,
                };
                await _user.CreateAsync(studentUser, studentObj.Password);
                await _user.AddToRoleAsync(studentUser, "Student");


                // Add to Student Contexted object and make a list of them
                var s = new Student {
                    StudentUID = studentObj.StudentUID,
                    Name = studentObj.FirstName + " " + studentObj.LastName,
                    Email = studentObj.Email,
                    PhoneNumber = studentObj.PhoneNumber,
                    CityOfOrigin = studentObj.CityOfOrigin,
                    StateOfOrigin = studentObj.StateOfOrigin,
                    CountryOfOrigin = studentObj.CountryOfOrigin,
                    Nationality = studentObj.Nationality,
                    Batch = SBatch,
                    Grade = SGrade,
                    School = school
                };
                studentList.Add(s);

            }
            await _context.Students.AddRangeAsync(studentList);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.AddBatch(BatchDTO batchObj, string schoolName) {
            var school = await InitSchool(schoolName);

            var students = new List<Student>();
            foreach(var stu in batchObj.Students) {
                var st = await _context.Students
                    .Include(s => s.School.Name)
                    .FirstOrDefaultAsync(s => s.Name == stu);
                if(st == null) {
                    return false;
                }
                students.Add(st);
            }

            // Make new batch from DTO
            var batch = new Batch {
                Name = batchObj.Name,
                Students = students
            };

            await _context.Batches.AddAsync(batch);
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
            var SBatch = await _context.Batches
                .FirstOrDefaultAsync(batch => batch.Name == studentObj.Batch);

            var sGrade = await _context.Grades
                .FirstOrDefaultAsync(grade => grade.Name == studentObj.Grade);

            var sSchool = await _context.Batches
                .FirstOrDefaultAsync(school => school.Name == studentObj.School);

            if (sGrade is null || sSchool is null) {
                return false;
            }
            var student = new Student {
                StudentUID = studentObj.StudentUID,
                Name = studentObj.Name,
                Email = studentObj.Email,
                PhoneNumber = studentObj.PhoneNumber,
                CityOfOrigin = studentObj.CityOfOrigin,
                StateOfOrigin = studentObj.StateOfOrigin,
                CountryOfOrigin = studentObj.CountryOfOrigin,
                Nationality = studentObj.Nationality,
                Batch = SBatch,
                BatchId = SBatch?.Id
            };

            //replace student from db
            _context.Remove(s);
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.DeleteStudent(string SUID, string schoolName) {
            var student = await _context.Students.FirstOrDefaultAsync(student =>  student.StudentUID == SUID);

            if(student == null) {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
