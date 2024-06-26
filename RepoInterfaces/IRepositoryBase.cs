using System.Linq.Expressions;

namespace RepoInterfaces;
public interface IRepositoryBase<T> where T : class
{
    T Get(params object[] id);
    Task<T> GetAsync(params object[] id);
    IQueryable<T> Set(Expression<Func<T, bool>> predicate);
    IQueryable<T> Set();
    Task<IEnumerable<T>> SetAsync();
    void Insert(T entity);
    void Update(T entity);
    void InsertOrUpdate(T entity);
    void Delete(object id);
    void Delete(T entity);
}
