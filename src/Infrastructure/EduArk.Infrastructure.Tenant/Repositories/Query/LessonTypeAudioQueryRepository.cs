using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class LessonTypeAudioQueryRepository : QueryRepository<LessonTypeAudio>, ILessonTypeAudioQueryRepository
    {
        public LessonTypeAudioQueryRepository(TenantDbContext context) : base(context) 
        { 
        
        }  
    }
}
