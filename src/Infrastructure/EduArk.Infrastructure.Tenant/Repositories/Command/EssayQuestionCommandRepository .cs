using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Command.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduArk.Infrastructure.Tenant.Repositories.Command
{
    public class EssayQuestionCommandRepository : CommandRepository<EssayQuestion>, IEssayQuestionCommandRepository
    {
        public EssayQuestionCommandRepository(TenantDbContext context): base(context)
        {

        }
    }
}
