using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T10, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, KeyProp>> LKey, Expression<Func<T10, KeyProp>> RKey) where T10 : class, IEntity
    {
        throw new Exception("求求了，别再联表了");
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T10, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, KeyProp>> LKey, Expression<Func<T10, KeyProp>> RKey) where T10 : class, IEntity
    {
        throw new Exception("求求了，别再联表了");
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T9, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, KeyProp>> LKey, Expression<Func<T9, KeyProp>> RKey) where T9 : class, IEntity
    {
        var rquery = repo.Query<T9>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, x.Data8, y })
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
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T9, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, KeyProp>> LKey, Expression<Func<T9, KeyProp>> RKey) where T9 : class, IEntity
    {
        var rquery = repo.Query<T9>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>
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
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> LeftJoin<T8, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>, KeyProp>> LKey, Expression<Func<T8, KeyProp>> RKey) where T8 : class, IEntity
    {
        var rquery = repo.Query<T8>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, x.Data7, y })
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
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> Join<T8, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>, KeyProp>> LKey, Expression<Func<T8, KeyProp>> RKey) where T8 : class, IEntity
    {
        var rquery = repo.Query<T8>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = x.Data7,
            Data8 = y,
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>, KeyProp>> LKey, Expression<Func<T7, KeyProp>> RKey) where T7 : class, IEntity
    {
        var rquery = repo.Query<T7>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, x.Data6, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = y,
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> Join<T7, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5, T6>, KeyProp>> LKey, Expression<Func<T7, KeyProp>> RKey) where T7 : class, IEntity
    {
        var rquery = repo.Query<T7>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = x.Data6,
            Data7 = y,
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2, T3, T4, T5>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4, T5>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> LeftJoin<T6, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5>, KeyProp>> LKey, Expression<Func<T6, KeyProp>> RKey) where T6 : class, IEntity
    {
        var rquery = repo.Query<T6>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, x.Data5, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = y,
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> Join<T6, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4, T5>, KeyProp>> LKey, Expression<Func<T6, KeyProp>> RKey) where T6 : class, IEntity
    {
        var rquery = repo.Query<T6>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5, T6>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = x.Data5,
            Data6 = y,
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2, T3, T4>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4>>? Query;
    public GroupJoinResult<T1, T2, T3, T4, T5> LeftJoin<T5, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4>, KeyProp>> LKey, Expression<Func<T5, KeyProp>> RKey) where T5 : class, IEntity
    {
        var rquery = repo.Query<T5>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, x.Data4, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = y,
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4, T5> Join<T5, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3, T4>, KeyProp>> LKey, Expression<Func<T5, KeyProp>> RKey) where T5 : class, IEntity
    {
        var rquery = repo.Query<T5>();
        var result = new GroupJoinResult<T1, T2, T3, T4, T5>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4, T5>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = x.Data4,
            Data5 = y,
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2, T3>
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public IQueryable<GroupJoinResultItem<T1, T2, T3>>? Query;
    private readonly IRepository repo;

    public GroupJoinResult<T1, T2, T3, T4> LeftJoin<T4, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3>, KeyProp>> LKey, Expression<Func<T4, KeyProp>> RKey) where T4 : class, IEntity
    {
        var rquery = repo.Query<T4>();
        var result = new GroupJoinResult<T1, T2, T3, T4>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, x.Data3, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3, T4>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = y
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3, T4> Join<T4, KeyProp>(Expression<Func<GroupJoinResultItem<T1, T2, T3>, KeyProp>> LKey, Expression<Func<T4, KeyProp>> RKey) where T4 : class, IEntity
    {
        var rquery = repo.Query<T4>();
        var result = new GroupJoinResult<T1, T2, T3, T4>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3, T4>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = x.Data3,
            Data4 = y
        });
        return result;
    }
}
public class GroupJoinResult<T1, T2>
{
    private readonly IRepository repo;
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public IQueryable<GroupJoinResultItem<T1, T2>>? Query;
    public GroupJoinResult<T1, T2, T3> LeftJoin<T3>(Expression<Func<GroupJoinResultItem<T1, T2>, object>> LKey, Expression<Func<T3, object>> RKey) where T3 : class, IEntity
    {
        var rquery = repo.Query<T3>();
        var result = new GroupJoinResult<T1, T2, T3>(repo);
        result.Query = Query?.GroupJoin(rquery, LKey, RKey, (x, y) => new { x.Data1, x.Data2, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2, T3>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = y
        });
        return result;
    }
    public GroupJoinResult<T1, T2, T3> Join<T3>(Expression<Func<GroupJoinResultItem<T1, T2>, object>> LKey, Expression<Func<T3, object>> RKey) where T3 : class, IEntity
    {
        var rquery = repo.Query<T3>();
        var result = new GroupJoinResult<T1, T2, T3>(repo);
        result.Query = Query?.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2, T3>
        {
            Data1 = x.Data1,
            Data2 = x.Data2,
            Data3 = y,
        });
        return result;
    }
}
public class GroupJoinResult<T1> where T1 : class
{
    private readonly IRepository repo;
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    public IQueryable<GroupJoinResultItem<T1>>? Query;
    public GroupJoinResult<T1, T2> LeftJoin<T2>(Expression<Func<T1, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity
    {
        var lquery = repo.Query<T1>();
        var rquery = repo.Query<T2>();
        var result = new GroupJoinResult<T1, T2>(repo);
        result.Query = lquery.GroupJoin(rquery, LKey, RKey, (x, y) => new { x, y })
        .SelectMany(x => x.y.DefaultIfEmpty(), (x, y) => new GroupJoinResultItem<T1, T2>
        {
            Data1 = x.x,
            Data2 = y
        });
        return result;
    }
    public GroupJoinResult<T1, T2> Join<T2>(Expression<Func<T1, object>> LKey, Expression<Func<T2, object>> RKey) where T2 : class, IEntity
    {
        var lquery = repo.Query<T1>();
        var rquery = repo.Query<T2>();
        var result = new GroupJoinResult<T1, T2>(repo);
        result.Query = lquery.Join(rquery, LKey, RKey, (x, y) => new GroupJoinResultItem<T1, T2>
        {
            Data1 = x,
            Data2 = y
        });
        return result;
    }
}