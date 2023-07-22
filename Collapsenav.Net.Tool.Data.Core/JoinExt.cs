using System.Linq.Expressions;

namespace Collapsenav.Net.Tool.Data;


public static class JoinExt
{
    public static GroupJoinResult<T> CreateJoin<T>(this IRepository<T> repo) where T : class, IEntity
    {
        var result = new GroupJoinResult<T>(repo);
        result.Query = repo.Query().Select(item => new GroupJoinResultItem<T> { Data1 = item });
        return result;
    }
}