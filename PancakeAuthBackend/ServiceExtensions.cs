using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;

namespace PancakeAuthBackend {
    public static class ServiceExtensions {
        public static void ConfigureIdentity(this IServiceCollection services) {

            var builder = services.AddIdentityCore<User>(x => {
                x.User.RequireUniqueEmail = true;
                x.Password.RequiredLength = 8;
                x.Password.RequireUppercase = true;
                x.Password.RequireDigit = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            builder.AddEntityFrameworkStores<BackendDataContext>();
            builder.AddDefaultTokenProviders();
        }

        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration Configuration) {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }) 
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        Configuration["Jwt:Key"]!))
                };
            });
        }

        public static void ConfigureAuthorization(this IServiceCollection services) {
            services.AddAuthorization(op => {
                op.AddPolicy("AdminAccess", policy => policy.RequireRole("SuperAdmin"));

                op.AddPolicy("SchoolAccess", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("SuperAdmin")
                     || context.User.IsInRole("SchoolManager")
                ));

                op.AddPolicy("StudentAccess", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("SuperAdmin")
                     || context.User.IsInRole("SchoolManager")
                     || context.User.IsInRole("Student")
                ));
            });
        }

        public static void AddSwaggerDoc(this IServiceCollection services) {
            services.AddSwaggerGen(op => {
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization Header using the Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>{"api.read"}
                    }
                });
                op.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "PancakeAuthBackend",
                    Version = "v1" 
                });
            });
        }

        async public static Task SeedIdentity(this IServiceScope scope) {

            var db = scope.ServiceProvider.GetRequiredService<BackendDataContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var password = config.GetSection("Passwords").GetValue<string>("SuperAdmin");

            //roles
            var SuperAdminRole = new IdentityRole("SuperAdmin");

            if (!db.Roles.Any()) {
                await roleManager.CreateAsync(SuperAdminRole);
            }

            var SchoolManagerRole = new IdentityRole("SchoolManager");

            if (!db.Roles.Any(r => r.Name == "SchoolManager")) {
                await roleManager.CreateAsync(SchoolManagerRole);
            }

            var StudentRole = new IdentityRole("Student");

            if (!db.Roles.Any(r => r.Name == "Student")) {
                await roleManager.CreateAsync(StudentRole);
            }

            //users

            //super-admin seeding
            if (!db.Users.Any(u => u.UserName == "SuperAdmin")) {
                var SuperAdminUser = new User {
                    UserName = "SuperAdmin",
                    Email = "superadmin@auth.com"
                };
                var sa = await userManager.CreateAsync(SuperAdminUser, password);
                await userManager.AddToRoleAsync(SuperAdminUser, SuperAdminRole.Name);
            }

            //school manager seeding

            if (!db.Users.Any(u => u.UserName == "HersheySchoolManager")) {
                var SchoolManagerUser = new User {
                    UserName = "HersheySchoolManager",
                    Email = "HersheySchoolManager@auth.com"
                };
                var sm = await userManager.CreateAsync(SchoolManagerUser, password);
                await userManager.AddToRoleAsync(SchoolManagerUser, SchoolManagerRole.Name);
                await userManager.AddClaimAsync(SchoolManagerUser, new Claim("School", "Hershey"));
            }
        }
    }
}
