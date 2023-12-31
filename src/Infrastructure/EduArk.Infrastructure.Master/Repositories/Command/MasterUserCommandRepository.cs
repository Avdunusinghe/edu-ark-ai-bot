using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Command.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Command.Base;

namespace EduArk.Infrastructure.Master.Repositories.Command
{
    public class MasterUserCommandRepository : CommandRepository<MasterUser>, IMasterUserCommandRepository
    {
        public MasterUserCommandRepository(MasterDbContext context)
           : base(context)
        {

        }
    }
}
