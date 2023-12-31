using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Repositories.Query.Tenant;
using EduArk.Infrastructure.Tenant.Data;
using EduArk.Infrastructure.Tenant.Repositories.Query.Base;

namespace EduArk.Infrastructure.Tenant.Repositories.Query
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(TenantDbContext context)
        : base(context)
        {

        }

        public async Task<IQueryable<User>> GetAllListOfUsersAsync(CancellationToken cancellationToken)
        {
            return  _context.Users;
        }
    }
}
