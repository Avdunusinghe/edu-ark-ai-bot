using EduArk.Domain.Repositories.Query.Base;

namespace EduArk.Domain.Repositories.Query.Master
{
    public interface ITenantCompanyQueryRepository : IQueryRepository<TenantCompany>
    {
        Task<IQueryable<TenantCompany>> GetAllListOfTenantCompaniesAsync(CancellationToken cancellationToken);
    }
}
