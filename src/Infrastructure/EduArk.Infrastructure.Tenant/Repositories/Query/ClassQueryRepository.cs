using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class ClassQueryRepository : QueryRepository<Class>, IClassQueryRepository
    {
        public ClassQueryRepository(TenantDbContext context) : base(context)
        {

        }
    }
}
