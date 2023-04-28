using Microsoft.AspNetCore.Identity;

namespace PancakeAuthBackend {
    public static class ServiceExtensions {
    
        public static void ConfigureIdentity(this IServiceCollection services) {

            var builder = services.AddIdentityCore<User>(x => x.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<BackendDataContext>().AddDefaultTokenProviders();
        }
    }
}
