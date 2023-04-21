using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace PancakeAuthBackend.Services {
    public class SchoolService : ISchoolService {
        readonly BackendDataContext _context;
        public SchoolService(BackendDataContext context) {
            _context = context;
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
            var school = await _context.Schools
                .Include(sc => sc.Subscriptions)
                .SingleOrDefaultAsync(s => s.Name == name);

            if (school == null) {
                return null;
            }
            school!.Address = _context.Addresses.FirstOrDefault(address => address.Id == school.AddressId) ?? null!;

            var subs = new List<string>();
            if(school.Subscriptions != null) {
                foreach(var subsciption in school.Subscriptions) {
                    if(subsciption != null) {
                        subs.Add(subsciption.Name);
                    }

                }
            }

            var schoolObj = new SchoolDTO {
                Name = school.Name,
                Subscriptions = subs,
                StreetName = school.Address.StreetName,
                City = school.Address.City,
                Region = school.Address.Region,
                State = school.Address.State,
                PostalCode = school.Address.PostalCode,
                Country = school.Address.Country
            };

            return schoolObj;
        }

        async public Task<List<StudentDTO>> GetSchoolStudents(string schoolName) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(s => s.Batch)
                    .Where(student => student.School == school)
                    .ToList();
                foreach (var student in students) {
                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = student.Batch != null ? student.Batch.Name : "None",
                        School = student.School.Name,
                        Grade = student.Grade.Name
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        } 

        async public Task<List<StudentDTO>> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(s => s.Batch)
                    .Where(student => student.School == school)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                foreach (var student in students) {
                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = student.Batch != null ? student.Batch.Name : "None",
                        School = student.School.Name,
                        Grade = student.Grade.Name
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByGrade(string schoolName, string grade) {
            var school =await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.School)
                    .Include(s => s.Grade)
                    .Where(student =>
                         student.School == school
                      && student.Grade.Name == grade
                    ).ToList();
                foreach (var student in students) {

                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = student.Batch == null ? "None" : student.Batch.Name,
                        Grade = grade,
                        School = schoolName
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByGradePaged(string schoolName, string grade, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(s => s.Batch)
                    .Include(s => s.School)
                    .Include(s => s.Grade)
                    .Where(student =>
                         student.School == school
                      && student.Grade.Name == grade
                    )
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                foreach (var student in students) {

                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = student.Batch == null ? "None" : student.Batch.Name,
                        Grade = grade,
                        School = schoolName
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByBatch(string schoolName, string batch) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(stu => stu.Batch)
                    .Where(student => student.Batch == null 
                        && student.Batch!.Name == batch)
                    .ToList();
                foreach (var student in students) {
                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = batch,
                        School = schoolName
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        async Task<List<StudentDTO>> ISchoolService.GetSchoolStudentByBatchPaged(string schoolName, string batch, int pageIndex, int pageSize) {
            var school = await InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students
                    .Include(stu => stu.Batch)
                    .Where(student => student.Batch == null
                        && student.Batch!.Name == batch)
                   .Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize)
                    .ToList();
                foreach (var student in students) {
                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Batch = batch,
                        School = schoolName
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        async Task<List<SubscriptionDTO>> ISchoolService.GetSchoolSubscriptions(string schoolName) {
            var school = InitSchool(schoolName);
            var subscriptionObjects = new List<SubscriptionDTO>();
            if (school != null) {
                var subscriptions = await _context.Schools
                    .Include(s => s.Subscriptions)
                    .Select(s => s.Subscriptions)
                    .FirstOrDefaultAsync(s => s != null && s.Equals(school));

                if(subscriptions is null) {
                    return new List<SubscriptionDTO>();
                }

                foreach(var subscription in subscriptions) {
                    var subscriptionObj = new SubscriptionDTO {
                        Type = subscription.Type,
                        Name = subscription.Name,
                        Description = subscription.Description,
                        IncludedChapters = subscription.IncludedChapters.Select(c => c.Title).ToList(),
                    };
                    subscriptionObjects.Add(subscriptionObj);
                }
            }
            return subscriptionObjects;
        }

        async Task<List<BillingDTO>> ISchoolService.GetSchoolBillings(string schoolName) {
            var school = await InitSchool(schoolName);
            var payments = new List<BillingDTO>();
            var pays = _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .ToList();

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
            var pays = _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

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

        async Task<bool> ISchoolService.AddStudents(List<StudentDTO> studentObjects, string batchName) {
         
            var studentList = new List<Student>();
            foreach (var studentObj in studentObjects) {
                var SBatch = await _context.Batches
                    .FirstOrDefaultAsync(batch => batch.Name == batchName);

                if (SBatch == null) {
                    return false;
                }

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
                    Batch = SBatch!,
                    BatchId = SBatch!.Id,
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
