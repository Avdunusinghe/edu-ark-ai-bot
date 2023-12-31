using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Query.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Query.Base;

namespace EduArk.Infrastructure.Master.Repositories.Query
{
    public class MasterUserQueryRepository : QueryRepository<MasterUser>, IMasterUserQueryRepository
    {
        public MasterUserQueryRepository(MasterDbContext context)
        : base(context)
        {

        }
    }
}
