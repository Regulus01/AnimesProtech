using System.Linq.Expressions;
using AnimesProtech.Domain.Entities.Base;
using AnimesProtech.Domain.Interface.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace AnimesProtech.Infra.Data.Repository.Base;

public abstract class BaseRepository<TContext, TClass> : IBaseRepository where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ILogger<TClass> _logger;

    public BaseRepository(TContext context, ILogger<TClass> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public IEnumerable<T> Query<T>(Expression<Func<T, bool>>? filter = null, int? skip = null, int? take = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null) where T : class
    {
        var query = _context.Set<T>().AsQueryable();

        if (includes != null)
            query = includes(query);

        if (skip != null && take != null)
            query = query.Skip(skip.Value).Take(take.Value);

        if (filter != null)
            query = query.Where(filter);

        var result = query.ToList();

        return result;
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public bool SaveChanges()
    {
        UpdateAuditDates();

        try
        {
            var result = _context.SaveChanges();
            return result >= 1;
        }
        catch (Exception e)
        {
            _logger.LogCritical("An error occured while updating the database: {error}", e.Message);
            return false;
        }
    }

    private void UpdateAuditDates()
    {
        var entityEntries = _context.ChangeTracker.Entries();

        foreach (var entry in entityEntries)
            if (entry.Entity.GetType().BaseType == typeof(BaseEntity))
                ChangeEntityAtributtes(entry);
    }

    private static void ChangeEntityAtributtes(EntityEntry entry)
    {
        if (entry.State == EntityState.Added)
        {
            var entity = entry.Entity as BaseEntity;
            entity?.ModificarDataDeCriacao(DateTimeOffset.UtcNow);
        }

        if (entry.State == EntityState.Modified)
        {
            var entity = entry.Entity as BaseEntity;
            entity?.ModificarDataDeAlteracao(DateTimeOffset.UtcNow);
        }
    }
}