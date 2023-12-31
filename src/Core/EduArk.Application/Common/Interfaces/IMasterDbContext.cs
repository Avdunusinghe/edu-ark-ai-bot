using EduArk.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace EduArk.Application.Common.Interfaces
{
    public interface IMasterDbContext
    {
        DbSet<AppSetting> AppSettings { get; }
        DbSet<TenantCompany> Tenants { get; }
        DbSet<MasterUser> MasterUsers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
        Task RetryOnExceptionAsync(Func<Task> func);
    }
}
