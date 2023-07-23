using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbContext _db;
    protected readonly DbSet<T> dbSet;
    public Repository(DbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    /// <summary>
    /// 查询所有符合条件的数据
    /// </summary>
    /// <param name="exp">筛选条件
    /// PS:若使用默认的NULL，则返回所有数据
    /// </param>
    public virtual IQueryable<T> Query(Expression<Func<T, bool>>? exp = null)
    {
        return exp == null ? dbSet.AsNoTracking().AsQueryable() : dbSet.AsNoTracking().Where(exp);
    }
    public virtual IQueryable<T> QueryWithTrack(Expression<Func<T, bool>>? exp = null)
    {
        return exp == null ? dbSet.AsQueryable() : dbSet.Where(exp);
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public int Save()
    {
        var count = _db.SaveChanges();
        ClearTracker();
        return count;
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    public async Task<int> SaveAsync()
    {
        var count = await _db.SaveChangesAsync();
        ClearTracker();
        return count;
    }

    private void ClearTracker()
    {
#if NET6_0_OR_GREATER
        _db.ChangeTracker.Clear();
#else
        var entries = _db.ChangeTracker.Entries();
        entries.ForEach(item => item.State = EntityState.Detached);
#endif
    }
    /// <summary>
    /// 获取主键
    /// </summary>
    public Type KeyType()
    {
        var prop = KeyProp() ?? throw new Exception("");
        if (prop.PropertyType.IsGenericType)
            return prop.PropertyType.GenericTypeArguments.First();
        return prop.PropertyType;
    }

    public PropertyInfo KeyProp()
    {
        var types = typeof(T).AttrValues<KeyAttribute>();
        if (types.IsEmpty())
            throw new Exception("There's No KeyProp");
        var prop = types.First().Key;
        return prop;
    }

    public IQueryable<E> Query<E>(Expression<Func<E, bool>>? exp = null) where E : class
    {
        var set = _db.Set<E>();
        return exp == null ? set.AsQueryable() : set.Where(exp);
    }

    public IQueryable<E> QueryWithTrack<E>(Expression<Func<E, bool>>? exp = null) where E : class
    {
        var set = _db.Set<E>();
        return exp == null ? set.AsQueryable() : set.Where(exp);
    }

    public void Dispose()
    {
        TransManager.Remove(_db);
    }
}
public class Repository<TKey, T> : Repository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    public Repository(DbContext db) : base(db) { }
}
