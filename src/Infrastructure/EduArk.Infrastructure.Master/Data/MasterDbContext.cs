using EduArk.Application.Common.Interfaces;
using EduArk.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduArk.Infrastructure.Master.Data
{
    public class MasterDbContext : DbContext, IMasterDbContext
    {
        public MasterDbContext()
        {
            
        }
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RetryOnExceptionAsync(Func<Task> func)
        {
            throw new NotImplementedException();
        }

        public DbSet<AppSetting> AppSettings => Set<AppSetting>();

        public DbSet<TenantCompany> Tenants => Set<TenantCompany>();

        public DbSet<MasterUser> MasterUsers => Set<MasterUser>();
    }
}
