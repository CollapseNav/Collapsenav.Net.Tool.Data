using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Data;
public partial class ModifyRepository<T> : WriteRepository<T>, IModifyRepository<T>
    where T : class, IEntity, new()
{
    public ModifyRepository(DbContext db) : base(db)
    {
    }
    /// <summary>
    /// 添加数据(集合)
    /// </summary>
    /// <param name="entityList">新的数据集合</param>
    public virtual async Task<int> AddAsync(IEnumerable<T>? entityList)
    {
        if (entityList == null)
            return 0;
        foreach (var entity in entityList)
            entity.Init();
        await dbSet.AddRangeAsync(entityList);
        return entityList.Count();
    }
    /// <summary>
    /// 有条件地删除数据
    /// </summary>
    /// <param name="exp">筛选条件</param>
    /// <param name="isTrue">是否真删,暂时还无法处理软删除</param>
    public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>>? exp, bool isTrue = false)
    {
        if (exp == null)
            return 0;
        TransManager.CreateTranscation(_db);
        // TODO 处理软删除的情况
        return isTrue ? await dbSet.Where(exp).DeleteAsync() : 0;
    }
    /// <summary>
    /// 实现按需要只更新部分更新
    /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
    /// </summary>
    public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity)
    {
        TransManager.CreateTranscation(_db);
        return where == null ? 0 : await dbSet.Where(where).UpdateAsync(entity);
    }

    public virtual async Task<int> UpdateWithoutTransactionAsync(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity)
    {
        return where == null ? 0 : await dbSet.Where(where).UpdateAsync(entity);
    }
}
public partial class ModifyRepository<TKey, T> : ModifyRepository<T>, IModifyRepository<TKey, T>
    where T : class, IEntity<TKey>, new()
{
    protected IWriteRepository<TKey, T> Repo;
    public ModifyRepository(DbContext db) : base(db)
    {
        Repo = new WriteRepository<TKey, T>(_db);
    }

    /// <summary>
    /// 根据id删除数据
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <param name="isTrue">是否真删</param>
    public virtual async Task<int> DeleteAsync(IEnumerable<TKey>? id, bool isTrue = false)
    {
        if (id == null)
            return 0;
        TransManager.CreateTranscation(_db);
        if (isTrue)
            return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
        return await dbSet.Where(item => id.Contains(item.Id)).UpdateAsync(entity => new
        {
            LastModificationTime = DateTime.Now,
            IsDeleted = true
        });
    }

    public virtual async Task<bool> DeleteAsync(TKey? id, bool isTrue = false)
    {
        return await Repo.DeleteAsync(id, isTrue);
    }
}
