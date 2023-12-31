using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class SubjectStreamQueryRepository : QueryRepository<SubjectStream>, ISubjectStreamQueryRepository
    {
        public SubjectStreamQueryRepository(TenantDbContext context) : base(context)
        {

        }
    }
}
