using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Command
{
    internal class SubjectAcademicLevelCommandRepository : CommandRepository<SubjectAcademicLevel>, ISubjectAcademicLevelCommandRepository
    {
        public SubjectAcademicLevelCommandRepository(TenantDbContext context) : base(context)
        {

        }
    }
}
