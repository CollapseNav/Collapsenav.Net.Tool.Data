using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbContext _db;
    protected readonly DbSet<T> dbSet;
    private bool disposedValue;

    public Repository(DbContext db)
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
                // TODO: 释放托管状态(托管对象)
            }

            // TODO: 释放未托管的资源(未托管的对象)并重写终结器
            // TODO: 将大型字段设置为 null
            disposedValue = true;
        }
    }

    // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
    // ~Repository()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
public class Repository<TKey, T> : Repository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    public Repository(DbContext db) : base(db) { }
}
