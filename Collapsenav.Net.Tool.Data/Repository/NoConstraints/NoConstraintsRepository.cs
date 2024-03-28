using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public class NoConstraintsRepository<Context, T> : NoConstraintsRepository<T>, INoConstraintsRepository<Context, T> where T : class, IEntity where Context : DbContext
{
    public NoConstraintsRepository(Context db) : base(db) { }
}
public class NoConstraintsRepository<T> : INoConstraintsRepository<T> where T : class
{
    protected readonly DbContext _db;
    protected readonly DbSet<T> dbSet;
    protected bool disposedValue;

    public NoConstraintsRepository(DbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    public virtual IQueryable<T> Query(Expression<Func<T, bool>>? exp = null)
    {
        return exp == null ? dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().Where(exp);
    }
    public virtual IQueryable<T> QueryWithTrack(Expression<Func<T, bool>>? exp = null)
    {
        return exp == null ? dbSet.AsQueryable() : dbSet.Where(exp);
    }
    public virtual int Save()
    {
        var count = _db.SaveChanges();
        ClearTracker();
        TransManager.CommitTranscation(_db);
        return count;
    }
    public virtual async Task<int> SaveAsync()
    {
        var count = await _db.SaveChangesAsync();
        ClearTracker();
        TransManager.CommitTranscation(_db);
        return count;
    }

    /// <summary>
    /// 清除track跟踪
    /// </summary>
    private void ClearTracker()
    {
#if NET6_0_OR_GREATER
        _db.ChangeTracker.Clear();
#else
        var entries = _db.ChangeTracker.Entries();
        entries.ForEach(item => item.State = EntityState.Detached);
#endif
    }
    public virtual Type KeyType()
    {
        var prop = KeyProp() ?? throw new Exception("");
        if (prop.PropertyType.IsGenericType)
            return prop.PropertyType.GenericTypeArguments.First();
        return prop.PropertyType;
    }
    public virtual PropertyInfo KeyProp()
    {
        var types = typeof(T).AttrValues<KeyAttribute>();
        if (types.IsEmpty())
            throw new Exception("There's No KeyProp");
        var prop = types.First().Key;
        return prop;
    }

    protected object GetKeyValue(object input)
    {
        var keyType = KeyType();
        return keyType.Name switch
        {
            nameof(Int32) => int.Parse(input.ToString() ?? string.Empty),
            nameof(Int64) => long.Parse(input.ToString() ?? string.Empty),
            nameof(String) => input.ToString() ?? string.Empty,
            nameof(Guid) => Guid.Parse(input.ToString() ?? string.Empty),
            _ => keyType.HasMethod("Parse") ? (keyType.GetMethod("Parse")!.Invoke(null, new[] { input }) ?? string.Empty) : string.Empty,
        };
    }

    public virtual IQueryable<E> Query<E>(Expression<Func<E, bool>>? exp = null) where E : class
    {
        var set = _db.Set<E>();
        return exp == null ? set.AsQueryable() : set.Where(exp);
    }

    public virtual IQueryable<E> QueryWithTrack<E>(Expression<Func<E, bool>>? exp = null) where E : class
    {
        var set = _db.Set<E>();
        return exp == null ? set.AsQueryable() : set.Where(exp);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public virtual GroupJoinResult<T, T2> LeftJoin<T2>(Expression<Func<T, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class
    {
        return this.CreateJoin().LeftJoin(LKey, RKey);
    }

    public virtual GroupJoinResult<T, T2> Join<T2>(Expression<Func<T, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class
    {
        return this.CreateJoin().Join(LKey, RKey);
    }
}
