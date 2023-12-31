using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class SemesterQueryRepository : QueryRepository<Semester>, ISemesterQueryRepository
    {
        public SemesterQueryRepository(TenantDbContext context)
        : base(context)
        {

        }
    }
}
