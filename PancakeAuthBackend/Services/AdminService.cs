using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PancakeAuthBackend.Models;
using System.Reflection.Metadata.Ecma335;

namespace PancakeAuthBackend.Services {
    public class AdminService : IAdminService {
        readonly BackendDataContext _context;

        public AdminService(BackendDataContext context) {
            _context = context;
        }

        //Util
        async Task<List<Subscription>> GetSubscriptionsByName(ICollection<string> subNames) {
            var subsWithoutNullCheck = new List<Subscription?>();
            foreach (var subName in subNames) {
                subsWithoutNullCheck.Add(
                       await _context.Subscriptions
                       .FirstOrDefaultAsync(Subscription => Subscription.Name == subName)
                    );
            }
            var subs = subsWithoutNullCheck
                .Where(subs => subs != null)
                .Select(subs => subs!)
                .ToList();
            return subs;
        }

        //*************************************************************************************************************

        //in-service utility

        async public Task<bool> SchoolExists(string name)
            => await _context.Schools
            .AnyAsync(school => school.Name == name);



        //*************************************************************************************************************


        //services

        async Task<bool> IAdminService.AddSchool(SchoolDTO school) {
            var subscriptions = await GetSubscriptionsByName(school.Subscriptions);

            if (subscriptions.Count == 0) {
                if (!_context.Subscriptions.IsNullOrEmpty()) { 
                    subscriptions = new List<Subscription> {
                        await _context.Subscriptions
                            .FirstAsync()
                    };
                }
                else {
                    return false;
                }
            }

            var School = new School {
                Name = school.Name,
                Subscriptions = subscriptions,

                Address = new Address {
                    StreetName = school.StreetName,
                    City = school.City,
                    Region = school.Region,
                    State = school.State,
                    Country = school.Country,
                    PostalCode = school.PostalCode,
                }
            };
            await _context.Schools.AddAsync(School);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.DeleteSchool(string schoolName) {
            var school = await _context.Schools
                .Include(sc => sc.Subscriptions)
                .SingleOrDefaultAsync(school => school.Name == schoolName);

            if (school == null) {
                return false;
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.EditSchool(string schoolName, SchoolDTO schoolObj) {
            var school = await _context.Schools
                .Include(sc => sc.Subscriptions)
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
                Console.WriteLine(ex.Message);
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        async Task<List<SchoolDTO>> IAdminService.GetSchools() {
            var schools = new List<SchoolDTO>();
            var schoolRecords = await _context.Schools
                .Include(sc => sc.Address)
                .Include(sch => sch.Subscriptions)
                .ToListAsync();

            if (schoolRecords is null) {
                return schools;
            }

            foreach (var record in schoolRecords) {
                if (record is null) {
                    continue;
                }
                //get subscriptions list
                var subscipts = new List<string>();
                foreach (var subsc in record.Subscriptions ?? new List<Subscription>()) {
                    subscipts.Add(subsc.Name);
                }

                var school = new SchoolDTO {
                    Name = record.Name,
                    Subscriptions = subscipts,
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
                .Include(sch => sch.Subscriptions)
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
                //get subscriptions list
                var subscipts = new List<string>();
                foreach (var subsc in record.Subscriptions ?? new List<Subscription>()) {
                    subscipts.Add(subsc.Name);
                }

                var school = new SchoolDTO {
                    Name = record.Name,
                    Subscriptions = subscipts,
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

        async Task<List<SchoolDTO>> IAdminService.GetSchoolsForSubscription(string subscriptionName) {
            var schools = new List<SchoolDTO>();
            var schoolRecords = await _context.Schools
                .Include(sc => sc.Address)
                .Include(sch => sch.Subscriptions)
                .Where(school => school.Subscriptions != null && school.Subscriptions!
                    .Any(subscription => subscription.Name == subscriptionName))
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
                    Subscriptions = new List<string>(),
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

        async Task<List<SchoolDTO>> IAdminService.GetSchoolsForSubscriptionByPage(string subscriptionName, int pageIndex, int pageSize) {
            var schools = new List<SchoolDTO>();
            var schoolRecords = await _context.Schools
                .Include(sc => sc.Address)
                .Include(sch => sch.Subscriptions)
                .Where(school => school.Subscriptions != null && school.Subscriptions!
                    .Any(subscription => subscription.Name == subscriptionName))
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
                    Subscriptions = new List<string>(),
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

        async Task<List<SubscriptionDTO>> IAdminService.GetSubscriptions() {
            var subscriptions = new List<SubscriptionDTO>();
            var subscriptionRecords = await _context.Subscriptions
                .Include(sub => sub.IncludedChapters)
                .ToListAsync();

            if (subscriptionRecords is null) {
                return subscriptions;
            }

            foreach (var record in subscriptionRecords) {
                if (record is null) {
                    continue;
                }
                //get chapters list
                var chapters = new List<string>();
                foreach (var chapter in record.IncludedChapters) {
                    chapters.Add(chapter.Title);
                }

                var subscription = new SubscriptionDTO {
                    Type = record.Type,
                    Name = record.Name,
                    Description = record.Description,
                    IncludedChapters = chapters
                };
                subscriptions.Add(subscription);
            }
            return subscriptions;
        }

        async Task<List<SubscriptionDTO>> IAdminService.GetSubscriptionsByPage(int pageIndex, int pageSize) {
            var subscriptions = new List<SubscriptionDTO>();
            var subscriptionRecords = await _context.Subscriptions
                .Include(sub => sub.IncludedChapters)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (subscriptionRecords is null) {
                return subscriptions;
            }

            foreach (var record in subscriptionRecords) {
                if (record is null) {
                    continue;
                }
                //get chapters list
                var chapters = new List<string>();
                foreach (var chapter in record.IncludedChapters) {
                    chapters.Add(chapter.Title);
                }

                var subscription = new SubscriptionDTO {
                    Type = record.Type,
                    Name = record.Name,
                    Description = record.Description,
                    IncludedChapters = chapters
                };
                subscriptions.Add(subscription);
            }
            return subscriptions;
        }

        async Task<SubscriptionDTO?> IAdminService.FindSubscription(string name) {
            var subscription = await _context.Subscriptions
                .Include(s => s.IncludedChapters)
                .FirstOrDefaultAsync(sub => sub.Name == name);

            if (subscription == null) {
                return null;
            }

            var chapters = new List<string>();
            foreach (var chapter in subscription.IncludedChapters) {
                if (chapter is null) {
                    continue;
                }
                chapters.Add(chapter.Title);
            }

            return new SubscriptionDTO {
                Type = subscription!.Type,
                Name = subscription!.Name,
                Description = subscription!.Description,
                IncludedChapters = chapters
            };
        }

        async Task<bool> IAdminService.AddSubscription(SubscriptionDTO subscription) {

            //get chapters
            var chapters = new List<Chapter>();
            foreach (var chap in subscription.IncludedChapters) {
                var chapter = await _context.Chapters
                    .FirstOrDefaultAsync(c => c.Title == chap);
                if (chapter is not null) {
                    chapters.Add(chapter);
                }
            }

            var sub = new Subscription {
                Type = subscription.Type,
                Name = subscription.Name,
                Description = subscription.Description,
                IncludedChapters = chapters
            };

            await _context.Subscriptions.AddAsync(sub);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.UpdateSubscription(string subscriptionName, SubscriptionDTO subscription) {

            //get chapters
            var chapters = new List<Chapter>();
            foreach (var chap in subscription.IncludedChapters) {
                var chapter = await _context.Chapters
                    .FirstOrDefaultAsync(c => c.Title == chap);
                if (chapter is not null) {
                    chapters.Add(chapter);
                }
            }

            //find subscription
            var sub = await _context.Subscriptions
                .Include(subsc => subsc.IncludedChapters)
                .FirstOrDefaultAsync(s => s.Name == subscriptionName);
            if(sub is null) {
                return false;
            }
            //replace chapters
   
            sub.IncludedChapters = new List<Chapter>();

            sub.Name = subscription.Name;
            sub.Type = subscription.Type;
            sub.Description = subscription.Description;
            sub.IncludedChapters = chapters;

            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.DeleteSubscription(string subscriptionName) {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Name == subscriptionName);

            if (subscription is null) {
                return false;
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
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

        List<SubjectDetailsDTO> IAdminService.GetSubjects() {
            var subjects = _context.Subjects
                .Include(c => c.Chapters)
                .AsNoTracking()
                .ToList();

            var subjectObjs = new List<SubjectDetailsDTO>();
            foreach (var subject in subjects) {
                var chapterObjs = new List<ChapterDTO>();
                if(subject.Chapters is not null) {
                    foreach (var chapter in subject.Chapters) {
                        chapterObjs.Add(
                            new ChapterDTO {
                                Title = chapter.Title,
                                Description = chapter.Description
                            });
                    }
                }
              
                subjectObjs.Add(
                new SubjectDetailsDTO {
                    Name = subject.Name,
                    Chapters = chapterObjs
                });
            }
            return subjectObjs;
        }

        List<SubjectDetailsDTO> IAdminService.GetSubjectsByPage(int pageIndex, int pageSize) {
            var subjects = _context.Subjects
                .Include(c => c.Chapters)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();

            var subjectObjs = new List<SubjectDetailsDTO>();
            foreach (var subject in subjects) {
                var chapterObjs = new List<ChapterDTO>();
                if (subject.Chapters is not null) {
                    foreach (var chapter in subject.Chapters) {
                        chapterObjs.Add(
                            new ChapterDTO {
                                Title = chapter.Title,
                                Description = chapter.Description
                            });
                    }
                }

                subjectObjs.Add(
                new SubjectDetailsDTO {
                    Name = subject.Name,
                    Chapters = chapterObjs
                });
            }
            return subjectObjs;
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
