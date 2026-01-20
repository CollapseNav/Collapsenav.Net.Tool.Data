using System.Linq.Expressions;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;
public class EFDB<Context> : IDB<Context> where Context : DbContext
{
    private readonly Context db;
    public EFDB(Context dbContext)
    {
        db = dbContext;
    }

    public virtual T Add<T>(T? data) where T : class
    {
        return AddAsync(data).Result;
    }

    public virtual IEnumerable<T> Add<T>(IEnumerable<T>? datas) where T : class
    {
        return AddAsync(datas).Result;
    }

    public virtual async Task<T> AddAsync<T>(T? data) where T : class
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (data is IEntity entity)
            entity.Init();
        await db.AddAsync(data);
        return data;
    }

    public virtual async Task<IEnumerable<T>> AddAsync<T>(IEnumerable<T>? datas) where T : class
    {
        if (datas == null)
            throw new ArgumentNullException(nameof(datas));
        if (datas.First() is IEntity)
        {
            (datas as IEnumerable<IEntity>).ForEach(item => item.Init());
        }
        await db.AddRangeAsync(datas);
        return datas;
    }

    public virtual void BeginTransaction()
    {
        db.Database.BeginTransaction();
    }

    public virtual Task CommitAsync()
    {
        db.Database.CommitTransaction();
        throw new NotImplementedException();
    }

    public virtual int Delete<T>(Expression<Func<T, bool>>? exp, bool isTrue = false) where T : class
    {
        return DeleteAsync(exp, isTrue).Result;
    }

    public virtual Task<int> DeleteAsync<T>(Expression<Func<T, bool>>? exp, bool isTrue = false) where T : class
    {
        if (isTrue)
        {
            return db.Set<T>().Where(exp).DeleteFromQueryAsync();
        }
        else
        {
            var list = db.Set<T>().Where(exp).ToList();
            if (list.First() is IEntity entity)
            {
                (list as IEnumerable<IEntity>).ForEach(item => item.SoftDelete());
            }
            db.UpdateRange(list);
            return Task.FromResult(list.Count);
        }
    }

    public virtual int DeleteById<T, ID>(ID? id, bool isTrue = false) where T : class
    {
        return DeleteByIdAsync<T, ID>(id, isTrue).Result;
    }

    public virtual int DeleteByIds<T, ID>(IEnumerable<ID>? ids, bool isTrue = false) where T : class
    {
        return DeleteByIdsAsync<T, ID>(ids, isTrue).Result;
    }

    public virtual async Task<int> DeleteByIdAsync<T, ID>(ID? id, bool isTrue = false) where T : class
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
        var data = await db.Set<T>().FindAsync(id);
        if (data == null)
            return 0;
        if (isTrue)
        {
            db.Remove(data);
        }
        else
        {
            if (data is IEntity entity)
                entity.SoftDelete();
            db.Update(data);
        }
        return 1;
    }

    public virtual async Task<int> DeleteByIdsAsync<T, ID>(IEnumerable<ID>? ids, bool isTrue = false) where T : class
    {
        if (ids == null)
            throw new ArgumentNullException(nameof(ids));
        int sum = 0;
        foreach (var item in ids)
        {
            var data = await db.Set<T>().FindAsync(item);
            if (data == null)
                continue;
            sum++;
            if (isTrue)
            {
                db.Remove(data);
            }
            else
            {
                if (data is IEntity entity)
                    entity.SoftDelete();
                db.Update(data);
            }
        }
        return sum;
    }

    public virtual T FindById<T, ID>(ID? id) where T : class
    {
        return FindByIdAsync<T, ID>(id).Result;
    }

    public virtual IEnumerable<T> FindByIds<T, ID>(IEnumerable<ID>? ids) where T : class
    {
        return FindByIdsAsync<T, ID>(ids).Result;
    }

    public virtual async Task<T> FindByIdAsync<T, ID>(ID? id) where T : class
    {
        return await db.FindAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> FindByIdsAsync<T, ID>(IEnumerable<ID>? ids) where T : class
    {
        if (ids == null)
            throw new ArgumentNullException(nameof(ids));
        var list = new List<T>();
        foreach (var id in ids)
            list.Add(await db.FindAsync<T>(id));
        return list;
    }

    public virtual IQueryable<T> Query<T>(Expression<Func<T, bool>>? exp = null) where T : class
    {
        return db.Set<T>().Where(exp);
    }

    public virtual void Rollback()
    {
        throw new NotImplementedException();
    }

    public virtual int SaveChanges()
    {
        return db.SaveChanges();
    }

    public virtual Task<int> SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public virtual IQueryable<T> Set<T>() where T : class
    {
        return db.Set<T>();
    }

    public virtual T Update<T>(T? data) where T : class
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (data is IEntity entity)
            entity.Update();
        db.Update(data);
        return data;
    }

    public virtual IEnumerable<T> Update<T>(IEnumerable<T>? data) where T : class
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (data.First() is IEntity)
        {
            (data as IEnumerable<IEntity>).ForEach(item => item.Update());
        }
        db.BulkUpdate(data);
        return data;
    }

    public virtual Task<T> UpdateAsync<T>(T? data) where T : class
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (data is IEntity entity)
            entity.Update();
        db.Update(data);
        return Task.FromResult(data);
    }

    public virtual Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T>? data) where T : class
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (data.First() is IEntity)
        {
            (data as IEnumerable<IEntity>).ForEach(item => item.Update());
        }
        db.BulkUpdate(data);
        return Task.FromResult(data);
    }

    public T AddOrUpdate<T>(T? data) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> AddOrUpdateAsync<T>(T? data) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync<T>(Expression<Func<T, bool>>? where, Expression<Func<T, T>>? entity) where T : class
    {
        return db.Set<T>().Where(where).UpdateAsync(entity);
    }
}