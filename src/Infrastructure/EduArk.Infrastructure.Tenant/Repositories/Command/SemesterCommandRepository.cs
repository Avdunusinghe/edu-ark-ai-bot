using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Command
{
    public class SemesterCommandRepository : CommandRepository<Semester>, ISemesterCommandRepository
    {
        public SemesterCommandRepository(TenantDbContext context)
           : base(context)
        {

        }
    }
}
