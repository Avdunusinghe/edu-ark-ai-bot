using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Command
{
    internal class SubjectTargetSettingCommandRepository : CommandRepository<SubjectTargetSetting>, ISubjectTargetSettingCommandRepository
    {
        public SubjectTargetSettingCommandRepository(TenantDbContext context)
           : base(context)
        {

        }
    }
}
