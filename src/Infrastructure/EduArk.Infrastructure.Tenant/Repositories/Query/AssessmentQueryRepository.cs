using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class AssessmentQueryRepository : QueryRepository<Assessment>, IAssessmentQueryRepository
    {
        public AssessmentQueryRepository(TenantDbContext context) : base(context)
        {

        }
    }
}
