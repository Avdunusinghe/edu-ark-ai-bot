using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Base;

namespace EduArk.Domain.Repositories.Query.Tenant
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {

        Task<IQueryable<User>> GetAllListOfUsersAsync(CancellationToken cancellationToken);
    }
}
