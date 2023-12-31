using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class LessonTypeTextQueryRepository : QueryRepository<LessonTypeText>, ILessonTypeTextQueryRepository
    {
        public LessonTypeTextQueryRepository(TenantDbContext context) : base(context)
        { 
        
        }
    }
}
