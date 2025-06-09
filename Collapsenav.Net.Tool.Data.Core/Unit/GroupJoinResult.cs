using System.Linq.Expressions;
namespace Collapsenav.Net.Tool.Data;

public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> : JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> result) : base(result) { }
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T10>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object?>> LKey, Expression<Func<T10, object?>> RKey) where T10 : class => throw new Exception("求求了，别再联表了");
    /// <summary>
    /// 求求了，别再联表了
    /// </summary>
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T10>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9>, object?>> LKey, Expression<Func<T10, object?>> RKey) where T10 : class => throw new Exception("求求了，别再联表了");
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> : JoinResult<T1, T2, T3, T4, T5, T6, T7, T8>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4, T5, T6, T7, T8> result) : base(result) => this.repo = repo;
    private readonly IRepository repo;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> LeftJoin<T9>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object?>> LKey, Expression<Func<T9, object?>> RKey) where T9 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo, LeftJoin(repo.Query<T9>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Join<T9>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>, object?>> LKey, Expression<Func<T9, object?>> RKey) where T9 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(repo, Join(repo.Query<T9>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> : JoinResult<T1, T2, T3, T4, T5, T6, T7>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4, T5, T6, T7> result) : base(result) => this.repo = repo;
    private readonly IRepository repo;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> LeftJoin<T8>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object?>> LKey, Expression<Func<T8, object?>> RKey) where T8 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo, LeftJoin(repo.Query<T8>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8> Join<T8>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6, T7>, object?>> LKey, Expression<Func<T8, object?>> RKey) where T8 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7, T8>(repo, Join(repo.Query<T8>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2, T3, T4, T5, T6> : JoinResult<T1, T2, T3, T4, T5, T6>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4, T5, T6> result) : base(result) => this.repo = repo; private readonly IRepository repo;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> LeftJoin<T7>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6>, object?>> LKey, Expression<Func<T7, object?>> RKey) where T7 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo, LeftJoin(repo.Query<T7>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4, T5, T6, T7> Join<T7>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5, T6>, object?>> LKey, Expression<Func<T7, object?>> RKey) where T7 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6, T7>(repo, Join(repo.Query<T7>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2, T3, T4, T5> : JoinResult<T1, T2, T3, T4, T5>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4, T5> result) : base(result) => this.repo = repo;
    private readonly IRepository repo;
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> LeftJoin<T6>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5>, object?>> LKey, Expression<Func<T6, object?>> RKey) where T6 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo, LeftJoin(repo.Query<T6>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4, T5, T6> Join<T6>(Expression<Func<JoinResultItem<T1, T2, T3, T4, T5>, object?>> LKey, Expression<Func<T6, object?>> RKey) where T6 : class => new GroupJoinResult<T1, T2, T3, T4, T5, T6>(repo, Join(repo.Query<T6>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2, T3, T4> : JoinResult<T1, T2, T3, T4>
{
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3, T4> result) : base(result) => this.repo = repo;
    private readonly IRepository repo;
    public GroupJoinResult<T1, T2, T3, T4, T5> LeftJoin<T5>(Expression<Func<JoinResultItem<T1, T2, T3, T4>, object?>> LKey, Expression<Func<T5, object?>> RKey) where T5 : class => new GroupJoinResult<T1, T2, T3, T4, T5>(repo, LeftJoin(repo.Query<T5>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4, T5> Join<T5>(Expression<Func<JoinResultItem<T1, T2, T3, T4>, object?>> LKey, Expression<Func<T5, object?>> RKey) where T5 : class => new GroupJoinResult<T1, T2, T3, T4, T5>(repo, Join(repo.Query<T5>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2, T3> : JoinResult<T1, T2, T3>
{
    private readonly IRepository repo;
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2, T3> result) : base(result) => this.repo = repo;
    public GroupJoinResult<T1, T2, T3, T4> LeftJoin<T4>(Expression<Func<JoinResultItem<T1, T2, T3>, object?>> LKey, Expression<Func<T4, object?>> RKey) where T4 : class => new GroupJoinResult<T1, T2, T3, T4>(repo, LeftJoin(repo.Query<T4>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3, T4> Join<T4>(Expression<Func<JoinResultItem<T1, T2, T3>, object?>> LKey, Expression<Func<T4, object?>> RKey) where T4 : class => new GroupJoinResult<T1, T2, T3, T4>(repo, Join(repo.Query<T4>(), LKey, RKey));
}
public class GroupJoinResult<T1, T2> : JoinResult<T1, T2>
{
    private readonly IRepository repo;
    internal GroupJoinResult(IRepository repo, JoinResult<T1, T2> result) : base(result) => this.repo = repo;
    public GroupJoinResult<T1, T2, T3> LeftJoin<T3>(Expression<Func<JoinResultItem<T1, T2>, object?>> LKey, Expression<Func<T3, object?>> RKey) where T3 : class => new GroupJoinResult<T1, T2, T3>(repo, LeftJoin(repo.Query<T3>(), LKey, RKey));
    public GroupJoinResult<T1, T2, T3> Join<T3>(Expression<Func<JoinResultItem<T1, T2>, object?>> LKey, Expression<Func<T3, object?>> RKey) where T3 : class => new GroupJoinResult<T1, T2, T3>(repo, Join(repo.Query<T3>(), LKey, RKey));
}
public class GroupJoinResult<T1> : JoinResult<T1> where T1 : class
{
    private readonly IRepository repo;
    public GroupJoinResult(IRepository repo, JoinResult<T1>? result = null) : base(repo.Query<T1>()) => this.repo = repo;
    public GroupJoinResult<T1, T2> LeftJoin<T2>(Expression<Func<T1, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class => new GroupJoinResult<T1, T2>(repo, LeftJoin(repo.Query<T2>(), LKey, RKey));
    public GroupJoinResult<T1, T2> Join<T2>(Expression<Func<T1, object?>> LKey, Expression<Func<T2, object?>> RKey) where T2 : class => new GroupJoinResult<T1, T2>(repo, Join(repo.Query<T2>(), LKey, RKey));
}