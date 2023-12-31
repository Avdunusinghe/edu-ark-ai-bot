using EduArk.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduArk.Infrastructure.Master.Data
{
    public class MasterDbContextInitialiser
    {
        private readonly ILogger<MasterDbContextInitialiser> _logger;
        private readonly MasterDbContext _context;

        public MasterDbContextInitialiser(ILogger<MasterDbContextInitialiser> logger, MasterDbContext context)
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
                await SeedUserAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedUserAsync()
        {
            if (!_context.MasterUsers.Any())
            {
                var masterUser01 = new MasterUser()
                {
                    FirstName = "edu ark",
                    LastName = "edu ark",
                    Email = "admin@eduark.com",
                    MobileNumber = string.Empty,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    IsActive = true
                };

                _context.MasterUsers.Add(masterUser01);

                await _context.SaveChangesAsync();
            }
        }

    }
}
