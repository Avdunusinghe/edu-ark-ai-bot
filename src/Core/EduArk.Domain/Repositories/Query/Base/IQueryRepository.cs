using System.Linq.Expressions;

namespace EduArk.Domain.Repositories.Query.Base
{
    public interface IQueryRepository<T> where T : class
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<T> GetById(int id, CancellationToken cancellationToken);
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression);
    }
}
