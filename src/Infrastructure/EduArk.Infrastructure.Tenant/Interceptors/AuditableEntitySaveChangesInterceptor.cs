using EduArk.Application.Common.Interfaces;
using EduArk.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EduArk.Infrastructure.Tenant.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public AuditableEntitySaveChangesInterceptor
        (
          ICurrentUserService currentUserService,
          IDateTimeService dateTime)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (_currentUserService.UserId.HasValue)
                    {
                        entry.Entity.CreatedByUserId = (int?)_currentUserService.UserId;
                        entry.Entity.UpdatedByUserId = (int?)_currentUserService.UserId;
                    }
                    entry.Entity.CreatedDate = _dateTimeService.Now;
                    entry.Entity.UpdateDate = _dateTimeService.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    if (_currentUserService.UserId.HasValue)
                    {
                        entry.Entity.UpdatedByUserId = (int?)_currentUserService.UserId;
                    }
                    entry.Entity.UpdateDate = _dateTimeService.Now;
                }
            }
        }

    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }

}
