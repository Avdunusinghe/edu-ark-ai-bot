using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Command.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Command.Base;

namespace EduArk.Infrastructure.Master.Repositories.Command
{
    public class TenantCompanyCommandRepository : CommandRepository<TenantCompany>, ITenantCompanyCommandRepository
    {
        public TenantCompanyCommandRepository(MasterDbContext context)
           : base(context)
        {

        }
    }
}
