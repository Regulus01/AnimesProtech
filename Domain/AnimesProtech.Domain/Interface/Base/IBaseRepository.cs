using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimesProtech.Domain.Interface.Base;

public interface IBaseRepository
{
    public void Add<T>(T entity) where T : class;
    public IEnumerable<T> Query<T>(Expression<Func<T, bool>>? filter = null, int? skip = null, int? take = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null) where T : class;
    public void Update<T>(T entity) where T : class;
    public void Delete<T>(T entity) where T : class;
    public bool SaveChanges();
}