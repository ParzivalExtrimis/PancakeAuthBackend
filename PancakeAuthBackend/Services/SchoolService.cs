using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;
using System.Collections.Generic;

namespace PancakeAuthBackend.Services {
    public class SchoolService : ISchoolService {
        readonly BackendDataContext _context;
        public SchoolService(BackendDataContext context) {
            _context = context;
        }

        //util
        private School? InitSchool(string name) {
            return _context.Schools.FirstOrDefault(school => school.Name == name);
        }

        private async Task<Subject?> GetSubjectByName(string name) 
            => await _context.Subjects.FirstOrDefaultAsync(subject => subject.Name == name);

        private Task<Subject?> GetSubjectByNameWithBatch(string name)
            => _context.Subjects
            .Include(subject =>  subject.Batches)
            .FirstOrDefaultAsync(subject => subject.Name == name);


        //********************************************************************************

        //in-service utility methods
        public bool SchoolExists(string name) {
            return _context.Schools.Any(school => school.Name == name);
        }

        //********************************************************************************

        //service methods
        public School? GetSchoolData(string name) {
            var school = InitSchool(name);
            if (school == null) {
                return null;
            }
            school!.Address = _context.Addresses.FirstOrDefault(address => address.Id == school.AddressId) ?? null!;
            return school;
        }

        public List<StudentDTO> GetSchoolStudents(string schoolName) {
            var school = InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students.Where(student => student.School == school).ToList();
                foreach (var student in students) {
                    Grade? sGrade = _context.Grades.FirstOrDefault(grade => grade.Id == student.GradeId);
                    Batch? sBatch = _context.Batches.FirstOrDefault(batch => batch.Id == student.BatchId);
                    School? sSchool = _context.Schools.FirstOrDefault(school => school.Id == student.SchoolId);

                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Grade = sGrade != null ? sGrade.Name : "NULL",
                        Batch = sBatch != null ? sBatch.Name : "NULL",
                        School = sSchool != null ? sSchool.Name : "NULL",
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        public List<StudentDTO> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = InitSchool(schoolName);
            var studentObjects = new List<StudentDTO>();
            if (school != null) {
                var students = _context.Students.Where(student => student.School == school).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                foreach (var student in students) {
                    Grade? sGrade = _context.Grades.FirstOrDefault(grade => grade.Id == student.GradeId);
                    Batch? sBatch = _context.Batches.FirstOrDefault(batch => batch.Id == student.BatchId);
                    School? sSchool = _context.Schools.FirstOrDefault(school => school.Id == student.SchoolId);

                    var studentObj = new StudentDTO {
                        StudentUID = student.StudentUID,
                        Name = student.Name,
                        Email = student.Email,
                        PhoneNumber = student.PhoneNumber,
                        CityOfOrigin = student.CityOfOrigin,
                        StateOfOrigin = student.StateOfOrigin,
                        CountryOfOrigin = student.CountryOfOrigin,
                        Nationality = student.Nationality,
                        Grade = sGrade != null ? sGrade.Name : "NULL",
                        Batch = sBatch != null ? sBatch.Name : "NULL",
                        School = sSchool != null ? sSchool.Name : "NULL",
                    };
                    studentObjects.Add(studentObj);
                }
            }
            return studentObjects;
        }

        List<SubscriptionDTO> ISchoolService.GetSchoolSubscriptions(string schoolName) {
            var school = InitSchool(schoolName);
            var subscriptionObjects = new List<SubscriptionDTO>();
            if (school != null) {
                var subscriptions = _context.Subscriptions.Where(subscription => subscription.SchoolId == school.Id).ToList();
                foreach (var subscription in subscriptions) {
                    List<string> chapters = _context.Chapters.Where(chapter => chapter.SubscriptionId == subscription.Id)
                        .Select(chapter => chapter.Title).
                        ToList();

                    var subscriptionObj = new SubscriptionDTO {
                        Type = subscription.Type,
                        Name = subscription.Name,
                        Description = subscription.Description,
                        IncludedChapters = chapters,
                    };
                    subscriptionObjects.Add(subscriptionObj);
                }
            }
            return subscriptionObjects;
        }

        List<SubscriptionDTO> ISchoolService.GetSchoolSubscriptionsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = InitSchool(schoolName);
            var subscriptionObjects = new List<SubscriptionDTO>();
            if (school != null) {
                var subscriptions = _context.Subscriptions.Where(subscription => subscription.SchoolId == school.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                foreach (var subscription in subscriptions) {
                    List<string> chapters = _context.Chapters.Where(chapter => chapter.SubscriptionId == subscription.Id)
                        .Select(chapter => chapter.Title).
                        ToList();

                    var subscriptionObj = new SubscriptionDTO {
                        Type = subscription.Type,
                        Name = subscription.Name,
                        Description = subscription.Description,
                        IncludedChapters = chapters,
                    };
                    subscriptionObjects.Add(subscriptionObj);
                }
            }
            return subscriptionObjects;
        }

        List<BatchDTO> ISchoolService.GetSchoolBatches(string schoolName) {
            var school = InitSchool(schoolName);
            var batchObjects = new List<BatchDTO>();
            if (school != null) {
                var batches = _context.Batches.Where(batch => batch.SchoolId == school.Id).ToList();
                foreach (var batch in batches) {
                    List<string> subjects = _context.Subjects.Where(subject => subject.Batches.Any(b => b.Id == batch.Id))
                        .Select(subject => subject.Name)
                        .ToList();
                    string grade = _context.Grades.FirstOrDefault(grade => grade.Id == batch.GradeId)?.Name ?? "NULL";

                    var batchObj = new BatchDTO {
                        Name = batch.Name,
                        Grade = grade,
                        Subjects = subjects,
                    };
                    batchObjects.Add(batchObj);
                }
            }
            return batchObjects;
        }

        List<BatchDTO> ISchoolService.GetSchoolBatchesByPage(string schoolName, int pageIndex, int pageSize) {
            var school = InitSchool(schoolName);
            var batchObjects = new List<BatchDTO>();
            if (school != null) {
                var batches = _context.Batches.Where(batch => batch.SchoolId == school.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                foreach (var batch in batches) {
                    List<string> subjects = _context.Subjects.Where(subject => subject.Batches.Any(b => b.Id == batch.Id))
                        .Select(subject => subject.Name)
                        .ToList();
                    string grade = _context.Grades.FirstOrDefault(grade => grade.Id == batch.GradeId)?.Name ?? "NULL";

                    var batchObj = new BatchDTO {
                        Name = batch.Name,
                        Grade = grade,
                        Subjects = subjects,
                    };
                    batchObjects.Add(batchObj);
                }
            }
            return batchObjects;
        }

        List<Payment> ISchoolService.GetSchoolPayments(string schoolName) {
            var school = InitSchool(schoolName);
            var payments = new List<Payment>();
            payments = _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .ToList();
            return payments;
        }

        List<Payment> ISchoolService.GetSchoolPaymentsByPage(string schoolName, int pageIndex, int pageSize) {
            var school = InitSchool(schoolName);
            var payments = new List<Payment>();
            payments = _context.Payments
                .Where(payment => payment.SchoolId == school!.Id)
                .OrderByDescending(payment => payment.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return payments;
        }

        async Task<bool> ISchoolService.AddStudents(List<StudentDTO> studentObjects, string schoolName) {
            var school = InitSchool(schoolName);
            var studentList = new List<Student>();
            foreach (var studentObj in studentObjects) {
                var sGrade = await _context.Grades.FirstOrDefaultAsync(grade => grade.Name == studentObj.Grade);
                var SBatch = await _context.Batches.FirstOrDefaultAsync(batch => batch.Name == studentObj.Batch);
                var sSchool = await _context.Schools.FirstOrDefaultAsync(school => school.Name == schoolName);

                if (sGrade == null || SBatch == null || sSchool == null) {
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
                    Grade = sGrade!,
                    Batch = SBatch!,
                    School = sSchool!,
                    GradeId = sGrade.Id,
                    BatchId = SBatch.Id,
                    SchoolId = sSchool.Id
                };
                studentList.Add(s);

            }
            await _context.Students.AddRangeAsync(studentList);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.AddBatch(BatchDTO batchObj, string schoolName) {
            var school = InitSchool(schoolName);
            var Grade = await _context.Grades.FirstOrDefaultAsync(grade => grade.Name == batchObj.Grade);
            var School = await _context.Schools.FirstOrDefaultAsync(school => school.Name == schoolName);

            var subjects = new List<Subject>();
            foreach (var sub in batchObj.Subjects) {
                var Subject = await GetSubjectByName(sub);
                if (Subject == null) {
                    return false;
                }
                subjects.Add(Subject);
            }

            if (Grade == null || School == null) {
                return false;
            }

            // Make new batch from DTO
            var batch = new Batch {
                Name = batchObj.Name,
                Grade = Grade,
                School = School,
                Subjects = subjects,
                GradeId = Grade!.Id,
                SchoolId = School!.Id,
            };

            await _context.Batches.AddAsync(batch);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.EditStudent(StudentDTO studentObj, string schoolName) {
            var s = await _context.Students.FirstOrDefaultAsync(s => 
                s.Name == studentObj.Name
             || s.StudentUID == studentObj.StudentUID  
             || s.Email == studentObj.Email);

            if(s == null) { 
                return false;
            }
            //make new student from DTO
            var sGrade = await _context.Grades.FirstOrDefaultAsync(grade => grade.Name == studentObj.Grade);
            var SBatch = await _context.Batches.FirstOrDefaultAsync(batch => batch.Name == studentObj.Batch);
            var sSchool = await _context.Schools.FirstOrDefaultAsync(school => school.Name == schoolName);

            if (sGrade == null || SBatch == null || sSchool == null) {
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
                Grade = sGrade!,
                Batch = SBatch!,
                School = sSchool!,
                GradeId = sGrade.Id,
                BatchId = SBatch.Id,
                SchoolId = sSchool.Id
            };

            //replace student from db
            _context.Remove(s);
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.EditBatch(List<string> subjects, string batchName, string schoolName) {
            try {
                var batch = await _context.Batches
                    .Include(batch => batch.Subjects)
                    .SingleAsync(b => b.Name == batchName);

                //get subjects
                var subs = new List<Subject>();
                foreach (var sub in subjects) {
                    var Subject = await GetSubjectByName(sub);
                    if (Subject == null) {
                        return false;
                    }
                   
                    subs.Add(Subject);
                }

                //only possible edit made in Batch
                batch.Subjects = subs;

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> ISchoolService.DeleteStudent(string SUID, string schoolName) {
            var student = await _context.Students.SingleAsync(student =>  student.StudentUID == SUID);

            if(student == null) {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
