using Microsoft.EntityFrameworkCore;

namespace PancakeAuthBackend.Services {
    public class AdminService : IAdminService {
        readonly BackendDataContext _context;

        public AdminService(BackendDataContext context) {
            _context = context;
        }

        //Util
        async Task<List<Subscription>> GetSubscriptionsByName(ICollection<string> subNames) {
            var subsWithoutNullCheck = new List<Subscription?>();
            foreach(var subName in subNames) {
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

            if(subscriptions.Count == 0) {
                return false;
            }

            var School = new School {
                Name = school.Name,
                Subscriptions = subscriptions,

                Address = new Address {
                    SchoolName = school.Name,
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
            if(school == null) { 
                return false; 
            }

            //get and delete all students belonging to schoolName
            var students = _context.Students
                .Where(student => student.SchoolId == school.Id)
                .ToList();

            _context.Students.RemoveRange(students);

            //delete the batches belonging to the school
            var batches = _context.Batches
                .Where(batch => batch.SchoolId == school.Id)
                .ToList();

            _context.Batches.RemoveRange(batches);
            
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<bool> IAdminService.EditSchool(string schoolName, SchoolDTO schoolObj) {
            var school = await _context.Schools
                .Include(sc => sc.Subscriptions)
                .SingleOrDefaultAsync(school => school.Name == schoolName);

            if(school == null) {
                return false;
            }

            //set address
            var address = await _context.Addresses
                .SingleOrDefaultAsync(adr => adr.Id == school.AddressId) 
                ?? new Address();

            address.SchoolName = schoolObj.Name;
            address.StreetName = schoolObj.StreetName;
            address.City = schoolObj.City;
            address.Region = schoolObj.Region;
            address.State = schoolObj.State;
            address.PostalCode = schoolObj.PostalCode;
            address.Country = schoolObj.Country;

            //set subscriptions

            //remove all subscriptions from the school
            var subscipts = _context.Schools
                .Include(sc => sc.Subscriptions)
                .Where(sch => sch.Id == school.Id)
                .Select(s => s.Subscriptions)
                .AsEnumerable();
             
            
                
            foreach(var subscriptList in subscipts) {
                foreach(var subscript in subscriptList) {
                    if(subscript.Schools == null) {
                        subscript.Schools = _context.Schools
                        .Where(sc => sc.Subscriptions
                        .Any(s => s.Equals(subscript)))
                        .ToList();
                    }

                    if (subscript.Schools != null) {
                        subscript.Schools.Remove(school);
                    }
                }   
               
            }
                
            //add subscriptions from DTO
            foreach(var subscription in schoolObj.Subscriptions) {
                var sub = await _context.Subscriptions
                    .SingleOrDefaultAsync(s => s.Name == subscription);

                if(sub == null) {
                    return false;
                }

                if(!_context.Schools
                    .Include(sc => sc.Subscriptions)
                    .Any(sch => sch.Id == school.Id && sch.Subscriptions
                        .Any(s => s.Equals(sub)
                         )
                     )
                ) {
                    
                    var new_subscription = await _context.Subscriptions
                        .SingleAsync(s => s.Equals(sub));

                    if(new_subscription.Schools == null) {
                        new_subscription.Schools = new List<School>();
                    }
                    new_subscription.Schools.Add(school);
                
                }
            }

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
    }
}
