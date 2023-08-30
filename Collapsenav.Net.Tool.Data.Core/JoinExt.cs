namespace Collapsenav.Net.Tool.Data;
public static class JoinExt
{
    /// <summary>
    /// 创建Join
    /// </summary>
    public static GroupJoinResult<T> CreateJoin<T>(this IRepository<T> repo) where T : class, IEntity
    {
        var result = new GroupJoinResult<T>(repo) { Query = repo.Query().Select(item => new GroupJoinResultItem<T> { Data1 = item }) };
        return result;
    }
    /// <summary>
    /// 创建Join
    /// </summary>
    public static GroupJoinResult<T> StartJoin<T>(this IRepository<T> repo) where T : class, IEntity
    {
        var result = new GroupJoinResult<T>(repo) { Query = repo.Query().Select(item => new GroupJoinResultItem<T> { Data1 = item }) };
        return result;
    }
}