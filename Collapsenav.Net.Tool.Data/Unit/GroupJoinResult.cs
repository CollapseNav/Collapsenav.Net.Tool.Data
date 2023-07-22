using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;

public class GroupJoinResult<T1, T2, T3, T4> where T1 : class
{
    public GroupJoinResult(IRepository repo)
    {
        this.repo = repo;
    }
    private readonly IRepository repo;
    public IQueryable<GroupJoinResultItem<T1, T2, T3, T4>>? Query;
}
public class GroupJoinResult<T1, T2, T3> where T1 : class
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
}
public class GroupJoinResult<T1, T2> where T1 : class
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
}