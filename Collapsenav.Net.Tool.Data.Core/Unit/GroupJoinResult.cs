using System.Collections;
using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T10>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object>> LKey, Expression<Func<T10, object>> RKey) where T10 : class, IEntity
    {
        throw new Exception("求求了，别再联表了");
    }
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T10>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object>> LKey, Expression<Func<T10, object>> RKey) where T10 : class, IEntity
    {
        throw new Exception("求求了，别再联表了");
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T9>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object>> LKey, Expression<Func<T9, object>> RKey) where T9 : class, IEntity
    {
        var rquery = repo.Query<T9>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, x.Data8, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = x.Data7,
            Data8 = x.Data8,
            Data9 = y,
        })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T9>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object>> LKey, Expression<Func<T9, object>> RKey) where T9 : class, IEntity
    {
        var rquery = repo.Query<T9>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>(x) { Data9 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>> Query;

    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> LeftJoin<T8>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object>> LKey, Expression<Func<T8, object>> RKey) where T8 : class, IEntity
    {
        var rquery = repo.Query<T8>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = x.Data7,
            Data8 = y,
        })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> Join<T8>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object>> LKey, Expression<Func<T8, object>> RKey) where T8 : class, IEntity
    {
        var rquery = repo.Query<T8>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>(x) { Data8 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>, object>> LKey, Expression<Func<T7, object>> RKey) where T7 : class, IEntity
    {
        var rquery = repo.Query<T7>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = y,
        })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> Join<T7>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>, object>> LKey, Expression<Func<T7, object>> RKey) where T7 : class, IEntity
    {
        var rquery = repo.Query<T7>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>(x) { Data7 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5>, object>> LKey, Expression<Func<T6, object>> RKey) where T6 : class, IEntity
    {
        var rquery = repo.Query<T6>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = x.Data5,
                Data6 = y,
            })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> Join<T6>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5>, object>> LKey, Expression<Func<T6, object>> RKey) where T6 : class, IEntity
    {
        var rquery = repo.Query<T6>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6>(x) { Data6 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4, T5>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3, T4> : IQueryable<GroupJoinResultItem<T1, T2, T3, T4>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public required IQueryable<GroupJoinResultItem<T1, T2, T3, T4>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4, T5> LeftJoin<T5>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4>, object>> LKey, Expression<Func<T5, object>> RKey) where T5 : class, IEntity
    {
        var rquery = repo.Query<T5>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = x.Data4,
                Data5 = y,
            })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5> Join<T5>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4>, object>> LKey, Expression<Func<T5, object>> RKey) where T5 : class, IEntity
    {
        var rquery = repo.Query<T5>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5>(x) { Data5 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3, T4>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2, T3> : IQueryable<GroupJoinResultItem<T1, T2, T3>>
{
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public required IQueryable<GroupJoinResultItem<T1, T2, T3>> Query;
    private readonly IRepository repo;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3, T4> LeftJoin<T4>(Expression<Func<GroupJoinResultItem<T1, T2, T3>, object>> LKey, Expression<Func<T4, object>> RKey) where T4 : class, IEntity
    {
        var rquery = repo.Query<T4>();
        var result = new GroupJoinResult<T1, T2, T3, T4>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = x.Data3,
                Data4 = y
            })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4> Join<T4>(Expression<Func<GroupJoinResultItem<T1, T2, T3>, object>> LKey, Expression<Func<T4, object>> RKey) where T4 : class, IEntity
    {
        var rquery = repo.Query<T4>();
        var result = new GroupJoinResult<T1, T2, T3, T4>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4>(x) { Data4 = y })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2, T3>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1, T2> : IQueryable<GroupJoinResultItem<T1, T2>>
{
    private readonly IRepository repo;
    internal GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public required IQueryable<GroupJoinResultItem<T1, T2>> Query { get; set; }
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2, T3> LeftJoin<T3>(Expression<Func<GroupJoinResultItem<T1, T2>, object>> LKey, Expression<Func<T3, object>> RKey) where T3 : class, IEntity
    {
        var rquery = repo.Query<T3>();
        var result = new GroupJoinResult<T1, T2, T3>(repo)
        {
            Query = Query.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3>
            {
                Data1 = x.Data1,
                Data2 = x.Data2,
                Data3 = y
            })
        };
        return result;
    }
    public GroupJoinResult<T1, T2, T3> Join<T3>(Expression<Func<GroupJoinResultItem<T1, T2>, object>> LKey, Expression<Func<T3, object>> RKey) where T3 : class, IEntity
    {
        var rquery = repo.Query<T3>();
        var result = new GroupJoinResult<T1, T2, T3>(repo)
        {
            Query = Query.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3>(x) { Data3 = y, })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1, T2>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}
public class GroupJoinResult<T1> : IQueryable<GroupJoinResultItem<T1>> where T1 : class
{
    private readonly IRepository repo;
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public required IQueryable<GroupJoinResultItem<T1>> Query;
    public Type ElementType => Query.ElementType;
    public Expression Expression => Query.Expression;
    public IQueryProvider Provider => Query.Provider;
    public GroupJoinResult<T1, T2> LeftJoin<T2>(Expression<Func<T1, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity
    {
        var lquery = repo.Query<T1>();
        var rquery = repo.Query<T2>();
        var result = new GroupJoinResult<T1, T2>(repo)
        {
            Query = lquery.GroupJoin(rquery, LKey, RKey, (x, y) => new { x, y })
            .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2>
            {
                Data1 = x.x,
                Data2 = y
            })
        };
        return result;
    }
    public GroupJoinResult<T1, T2> Join<T2>(Expression<Func<T1, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity
    {
        var lquery = repo.Query<T1>();
        var rquery = repo.Query<T2>();
        var result = new GroupJoinResult<T1, T2>(repo)
        {
            Query = lquery.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2>(new GroupJoinResultItem<T1> { Data1 = x }) { Data2 = y })
        };
        return result;
    }
    public IEnumerator<GroupJoinResultItem<T1>> GetEnumerator()
    {
        return Query.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }
}