using EduArk.Domain.Entities.Master;
using EduArk.Domain.Repositories.Query.Master;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Master.Repositories.Query.Base;

namespace EduArk.Infrastructure.Master.Repositories.Query
{
    public class TenantCompanyQueryRepository : QueryRepository<TenantCompany>, ITenantCompanyQueryRepository
    {
        public TenantCompanyQueryRepository(MasterDbContext context)
        : base(context)
        {

        }

        public async Task<IQueryable<TenantCompany>> GetAllListOfTenantCompaniesAsync(CancellationToken cancellationToken)
        {
            return _context.Tenants;  
        }

       


    }
}
