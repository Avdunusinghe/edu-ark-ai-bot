using EduArk.Application.DTOs.AuthenticationDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Tenant.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Sentry;
using Sentry.Protocol;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using User = EduArk.Domain.Entities.Tenant.User;

namespace EduArk.API.Infrastructure.Middlewares
{
    public class TenantSelectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;


        public TenantSelectionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
            this._configuration = configuration;

        }

        public async Task Invoke(HttpContext httpContext, TenantDbContext tenantDbContext, MasterDbContext master)
        {
            try
            {
                // Retrieve available tenants from the master context
                var availableTenants = master.Tenants
                                      .Where(x => x.IsActive == true)
                                      .ToList();

                if (httpContext.Request != null)
                {

                    var identity = httpContext.User.Identity as ClaimsIdentity;

                    if (identity.Claims.Count() > 0)
                    {
                        // Retrieve the tenant based on the secret key in the identity claim
                        var tenant = availableTenants
                                    .FirstOrDefault(t => t.SecretKey.ToString() == identity.FindFirst("SecretKey").Value);

                        // Set the connection string for the tenant's database context
                        tenantDbContext.Database.SetConnectionString(tenant?.ConnectionString);

                        // Call the next middleware in the pipeline
                        await _next(httpContext);
                    }
                    else
                    {
                        // Read the request body to retrieve the authentication DTO
                        using (var reader = new StreamReader(
                            httpContext.Request.Body,
                            Encoding.UTF8))
                        {
                            var requestBody = await reader.ReadToEndAsync();

                            // Deserialize the authentication DTO from the request body
                            var authDto = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticationDTO>(requestBody);

                            if (authDto != null || authDto.Domain != null)
                            {
                                // Retrieve the tenant based on the domain from the authentication DTO
                                var tenant = availableTenants
                                    .FirstOrDefault(x => x.Domain.ToUpper().Trim() == authDto?.Domain.ToUpper().Trim());


                                // Set the connection string for the tenant's database context
                                tenantDbContext.Database.SetConnectionString(tenant?.ConnectionString);

                                if (tenantDbContext.Database.IsSqlServer())
                                {
                                    // Apply database migration for the tenant's database context
                                    await tenantDbContext.Database.MigrateAsync();

                                    // Seed users and roles for the tenant's database context
                                    await SeedUsersAndRolesAsync(tenantDbContext);

                                    await SeedSemesters(tenantDbContext);

                                    await SeedExamTypes(tenantDbContext);

                                    await SeedAcademicYear(tenantDbContext);
                                }

                                // Reset the request body with the original content
                                httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
                            }
                           

                        }
                        // Call the next middleware in the pipeline
                        await _next(httpContext);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and set the response status code to 400
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                HttpResponseWritingExtensions.WriteAsync(httpContext.Response, "{\"message\": " + ex.ToString() + "}").Wait();
            }
        }

        private async Task SeedUsersAndRolesAsync(TenantDbContext _context)
        {
            if (!_context.Roles.Any())
            {
                var adminRole = new Role()
                {

                    Name = "Admin"
                };

                var principalRole = new Role()
                {

                    Name = "Princlipal"
                };

                var levelHead = new Role()
                {

                    Name = "Level Head"
                };

                var hod = new Role()
                {

                    Name = "Head of Deparment"
                };

                var teacher = new Role()
                {

                    Name = " Teacher"
                };

                var student = new Role()
                {

                    Name = "Student"
                };

                var superAdmin = new Role()
                {

                    Name = "Super Admin"
                };


                _context.Roles.Add(adminRole);
                _context.Roles.Add(principalRole);
                _context.Roles.Add(levelHead);
                _context.Roles.Add(hod);
                _context.Roles.Add(teacher);
                _context.Roles.Add(student);
                _context.Roles.Add(superAdmin);


                await _context.SaveChangesAsync();

                if (!_context.Users.Any())
                {
                    var admin = new Domain.Entities.Tenant.User()
                    {

                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "admin@eduark.com",
                        Email = "admin@eduark.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass@123"),
                        PhoneNumber = "0112147852",
                        IsActive = true,
                        CreatedDate = DateTime.Now,


                    };

                    var principal = new User()
                    {

                        FirstName = "Princlipal",
                        LastName = "Princlipal",
                        UserName = "princlipal@eduark.com",
                        Email = "princlipal@eduark.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass@123"),
                        PhoneNumber = "0112147852",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                    };

                    _context.Users.Add(admin);
                    _context.Users.Add(principal);

                    if (!_context.UserRoles.Any())
                    {
                        var adminUserRole = new UserRole()
                        {
                            Role = adminRole,
                            User = admin,
                            CreatedDate = DateTime.Now,
                            CreatedByUser = admin,
                            UpdateDate = DateTime.Now,
                            UpdatedByUser = admin,
                            IsActive = true,

                        };

                        var managerUserRole = new UserRole()
                        {
                            Role = principalRole,
                            User = principal,
                            CreatedDate = DateTime.Now,

                            CreatedByUser = admin,
                            UpdateDate = DateTime.Now,
                            UpdatedByUser = admin,
                            IsActive = true,

                        };

                        _context.UserRoles.Add(adminUserRole);
                        _context.UserRoles.Add(managerUserRole);


                    }
                }

                await _context.SaveChangesAsync();
            }


        }

        private async Task SeedSemesters(TenantDbContext _context)
        {
            if (!_context.Semesters.Any())
            {
                var semsester1 = new Semester()
                {
                    Name = "Semester 1",
                };

                var semester2 = new Semester()
                {
                    Name = "Semester 2"
                };

                var semesters3 = new Semester()
                {
                    Name = "Semester 3"
                };

                _context.Semesters.Add(semsester1);
                _context.Semesters.Add(semester2);
                _context.Semesters.Add(semesters3);

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedExamTypes(TenantDbContext _context)
        {
            if (!_context.ExamTypes.Any())
            {
                var mothlyExam = new ExamType()
                {
                    Name = "Monthly Exam",
                };

                var midExam = new ExamType()
                {
                    Name = "Mid Exam"
                };

                var finalExam = new ExamType()
                {
                    Name = "Semester 3"
                };

                _context.ExamTypes.Add(mothlyExam);
                _context.ExamTypes.Add(midExam);
                _context.ExamTypes.Add(finalExam);

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedAcademicYear(TenantDbContext _context)
        {
            if (!_context.AcademicYears.Any())
            {
                var currentYear = new AcademicYear()
                {
                    Id = 2023,
                    CreatedDate = DateTime.Now,
                    CreatedByUserId = 1,
                    UpdateDate = DateTime.Now,
                    UpdatedByUserId = 1,
                    IsActive = true,
                    IsCurrentYear = true,
                };

                _context.AcademicYears.Add(currentYear);

                await _context.SaveChangesAsync();
            }
        }


    }
}
