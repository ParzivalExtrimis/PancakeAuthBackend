using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PancakeAuthBackend.Models;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace PancakeAuthBackend.Services {
    public class AdminService : IAdminService {
        readonly BackendDataContext _context;
        readonly UserManager<User> _user;
        readonly ILogger<AdminService> _log;

        public AdminService(BackendDataContext context, UserManager<User> user, ILogger<AdminService> log) {
            _context = context;
            _user = user;
            _log = log;
        }

        //*************************************************************************************************************

        //in-service utility

        async public Task<bool> SchoolExists(string name)
            => await _context.Schools
            .AnyAsync(school => school.Name == name);



        //*************************************************************************************************************


        //services

        async Task<bool> IAdminService.AddSchool(RegisterSchoolDTO school) {

            var user = new User {
                FirstName = school.ManagerFirstName,
                LastName = school.ManagerLastName,
                Email = school.ManagerEmail,
                PhoneNumber = school.ManagerPhone,
                ProfilePicture = school.ManagerPicture,
            };

            await _user.CreateAsync(user, school.ManagerPassword);
            await _user.AddToRoleAsync(user, "SchoolManager");
            await _user.AddClaimAsync(user, new Claim("School", school.Name));

            var School = new School {
                Name = school.Name,

                Address = new Address {
                    StreetName = school.StreetName,
                    City = school.City,
                    Region = school.Region,
                    State = school.State,
                    Country = school.Country,
                    PostalCode = school.PostalCode,
                },
                SchoolManager = user
            };
            await _context.Schools.AddAsync(School);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.DeleteSchool(string schoolName) {
            var school = await _context.Schools
                .SingleOrDefaultAsync(school => school.Name == schoolName);

            if (school == null) {
                return false;
            }

            var students = await _context.Students
                .Where(s => s.SchoolId == school.Id)
                .ToListAsync();

            if(students is not null) {
                _context.Students.RemoveRange(students);
                await _context.SaveChangesAsync();
            }
           
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.EditSchool(string schoolName, SchoolDTO schoolObj) {
            var school = await _context.Schools
                .FirstOrDefaultAsync(school => school.Name == schoolName);

            if (school == null) {
                return false;
            }

            //set address
            var address = await _context.Addresses
                .FirstOrDefaultAsync(adr => adr.Id == school.AddressId);

            if(address is null) {
                return false;
            }

            address.StreetName = schoolObj.StreetName;
            address.City = schoolObj.City;
            address.Region = schoolObj.Region;
            address.State = schoolObj.State;
            address.PostalCode = schoolObj.PostalCode;
            address.Country = schoolObj.Country;            

            //change school name if required
            try {
                school.Name = schoolObj.Name;
            }
            catch (Exception ex) {
                _log.LogError("Edit School Failed: {0}", ex.Message);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        async Task<List<SchoolDTO>> IAdminService.GetSchools() {
            var schools = new List<SchoolDTO>();
            var schoolRecords = await _context.Schools
                .Include(sc => sc.Address)
                .ToListAsync();

            if (schoolRecords is null) {
                return schools;
            }

            foreach (var record in schoolRecords) {
                if (record is null) {
                    continue;
                }

                var school = new SchoolDTO {
                    Name = record.Name,
                    StreetName = record.Address.StreetName,
                    City = record.Address.City,
                    Region = record.Address.Region,
                    State = record.Address.State,
                    PostalCode = record.Address.PostalCode,
                    Country = record.Address.Country
                };
                schools.Add(school);
            }
            return schools;
        }

        async Task<List<SchoolDTO>> IAdminService.GetSchoolsByPage(int pageIndex, int pageSize) {
            var schools = new List<SchoolDTO>();
            var schoolRecords = await _context.Schools
                .Include(sc => sc.Address)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (schoolRecords is null) {
                return schools;
            }

            foreach (var record in schoolRecords) {
                if (record is null) {
                    continue;
                }

                var school = new SchoolDTO {
                    Name = record.Name,
                    StreetName = record.Address.StreetName,
                    City = record.Address.City,
                    Region = record.Address.Region,
                    State = record.Address.State,
                    PostalCode = record.Address.PostalCode,
                    Country = record.Address.Country
                };
                schools.Add(school);
            }
            return schools;
        }

        List<ChapterDTO> IAdminService.GetChapters() {
            var chapters = _context.Chapters
                .Include(c => c.Subject)
                .AsNoTracking()
                .ToList();

            var chapterObjs = new List<ChapterDTO>();
            foreach(var chapter in chapters) {
                chapterObjs.Add(
                    new ChapterDTO {
                        Title = chapter.Title,
                        Description = chapter.Description,
                        Subject = chapter.Subject is null
                            ? "Unspecified"
                            : chapter.Subject!.Name
                    }); 
            }
            return chapterObjs;
        }

        List<ChapterDTO> IAdminService.GetChaptersByPage(int pageIndex, int pageSize) {
            var chapters = _context.Chapters
               .Include(c => c.Subject)
               .Skip((pageIndex - 1) * pageSize)
               .Take(pageSize)
               .AsNoTracking()
               .ToList();

            var chapterObjs = new List<ChapterDTO>();
            foreach (var chapter in chapters) {
                chapterObjs.Add(
                    new ChapterDTO {
                        Title = chapter.Title,
                        Description = chapter.Description,
                        Subject = chapter.Subject is null
                            ? "Unspecified"
                            : chapter.Subject!.Name
                    });
            }
            return chapterObjs;
        }

        async Task<ChapterDTO?> IAdminService.FindChapter(string name) {
            var chapter = await _context.Chapters
             .Include(c => c.Subject)
             .AsNoTracking()
             .FirstOrDefaultAsync(ch => ch.Title == name);

            if(chapter is null) {
                return null;
            }

            return new ChapterDTO {
                Title = chapter.Title,
                Description = chapter.Description,
                Subject = chapter.Subject is null
                            ? "Unspecified"
                            : chapter.Subject!.Name
            };
        }

        async Task<bool> IAdminService.AddChapters(List<ChapterDTO> chaptersToAdd) {

           var chapters = new List<Chapter>();
           foreach(var chapterToAdd in chaptersToAdd) {
                //subject
                Subject subject;
                if (await _context.Subjects.AsNoTracking().AnyAsync(s => s.Name == chapterToAdd.Subject)) {
                    subject = await _context.Subjects
                        .AsNoTracking()
                        .FirstAsync(s => s.Name == chapterToAdd.Subject);
                }
                else {
                    return false;
                }

                var chapter = new Chapter {
                    Title = chapterToAdd.Title,
                    Description = chapterToAdd.Description,
                    Subject = subject,
                    SubjectId = subject.Id
                };
                chapters.Add(chapter);
            }

            await _context.Chapters.AddRangeAsync(chapters);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.DeleteChapter(string chapterName) {
            if(await _context.Chapters.AnyAsync(c => c.Title == chapterName)) {

                var chapter = await _context.Chapters
                    .FirstAsync(c => c.Title == chapterName);

                _context.Chapters.Remove(chapter);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<List<ChapterDTO>> IAdminService.GetChaptersBySubject(string sub) {
            return await _context.Chapters
                .Include(c => c.Subject)
                .Where(c => c.Subject.Name == sub)
                .Select(chapter => new ChapterDTO {
                    Title = chapter.Title,
                    Description = chapter.Description
                })
                .ToListAsync()
                ?? new List<ChapterDTO>();
        }

        async Task<List<SubjectDTO>> IAdminService.GetSubjects() {
            return await _context.Subjects
                .Select(s => new SubjectDTO {
                    Name = s.Name
                })
                .ToListAsync();
        }

        async Task<List<SubjectDTO>> IAdminService.GetSubjectsByPage(int pageIndex, int pageSize) {
            return await _context.Subjects
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SubjectDTO {
                    Name = s.Name
                })
                .ToListAsync();
        }

        async Task IAdminService.AddSubjects(List<SubjectDTO> subs) {

            var subjects = new List<Subject>();
            foreach(var sub in subs) {
                subjects.Add(
                    new Subject {
                        Name = sub.Name
                    }
                );
            }
            await _context.Subjects.AddRangeAsync(subjects);
            await _context.SaveChangesAsync();
        }

        async Task<bool> IAdminService.DeleteSubject(string subjectName) {
            if (await _context.Subjects.AnyAsync(s => s.Name == subjectName)) {

                var subject = await _context.Subjects
                    .FirstAsync(s => s.Name == subjectName);

                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
