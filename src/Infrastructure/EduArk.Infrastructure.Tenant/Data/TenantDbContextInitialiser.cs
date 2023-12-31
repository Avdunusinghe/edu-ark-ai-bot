using EduArk.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduArk.Infrastructure.Tenant.Data
{
    public class TenantDbContextInitialiser
    {
        private readonly ILogger<TenantDbContextInitialiser> _logger;
        private readonly TenantDbContext _context;

        public TenantDbContextInitialiser(ILogger<TenantDbContextInitialiser> logger, TenantDbContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                //await SeedRolesAsync();
                //await SeedUsersAndRolesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task SeedRolesAsync()
        {
            if (!_context.Roles.Any())
            {
                var adminRole = new Role()
                {
                    Id = 1,
                    Name = "Admin"
                };

                var principalRole = new Role()
                {
                    Id = 2,
                    Name = "Princlipal"
                };

                var levelHead = new Role()
                {
                    Id = 3,
                    Name = "Level Head"
                };

                var hod = new Role()
                {
                    Id = 4,
                    Name = "Head of Deparment"
                };

                var teacher = new Role()
                {
                    Id = 5,
                    Name = " Teacher"
                };

                var student = new Role()
                {
                    Id = 6,
                    Name = "Student"
                };

                var superAdmin = new Role()
                {
                    Id = 7,
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
            }
        }

        private async Task SeedUsersAndRolesAsync()
        {


            if (!_context.Users.Any())
            {
                var admin = new User()
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
                        RoleId = 1,
                        User = admin,
                        CreatedDate = DateTime.Now,
                        CreatedByUser = admin,
                        UpdateDate = DateTime.Now,
                        UpdatedByUser = admin,
                        IsActive = true,

                    };

                    var managerUserRole = new UserRole()
                    {
                        RoleId = 2,
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


}
    

