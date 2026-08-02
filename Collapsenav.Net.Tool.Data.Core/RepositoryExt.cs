using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;

public static class RepositoryExt
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(IModifyRepository<>), typeof(ModifyRepository<>))
        .AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>))
        .AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
        .AddScoped(typeof(IEntityQueryRepository<>), typeof(EntityQueryRepository<>))
        .AddScoped(typeof(IEntityModifyRepository<>), typeof(EntityModifyRepository<>))
        .AddScoped(typeof(IEntityCrudRepository<>), typeof(EntityCrudRepository<>))
        .AddDefaultIdGenerator()
        ;
        return services;
    }

    private static readonly Type[] RepositoryInterfaceDefinitions = new[]
    {
        typeof(IRepository<>), typeof(IQueryRepository<>), typeof(IModifyRepository<>), typeof(ICrudRepository<>),
        typeof(IEntityRepository<>), typeof(IEntityQueryRepository<>), typeof(IEntityModifyRepository<>), typeof(IEntityCrudRepository<>),
    };

    /// <summary>
    /// 注册仓储并允许用自定义实现替换默认实现。
    /// 对传入的每个类型，反射其实现的仓储接口，只重映射"家族链最派生接口"（如 <see cref="IModifyRepository{T}"/>），
    /// 支持 open generic（<c>typeof(MyRepo&lt;&gt;)</c>）与 closed。
    /// </summary>
    public static IServiceCollection AddRepository(this IServiceCollection services, params Type[] types)
    {
        services.AddRepository();
        foreach (var type in types)
        {
            foreach (var iface in FindLeafRepositoryInterfaces(type))
                services.AddScoped(iface, type);
        }
        return services;
    }

    private static IEnumerable<Type> FindLeafRepositoryInterfaces(Type type)
    {
        var repoInterfaces = type.GetInterfaces()
            .Where(i => i.IsGenericType && RepositoryInterfaceDefinitions.Contains(i.GetGenericTypeDefinition()))
            .ToList();
        // 最派生接口：没有任何其他已实现接口继承它
        return repoInterfaces.Where(candidate =>
            !repoInterfaces.Any(other => other != candidate && other.GetInterfaces().Contains(candidate)));
    }


    /// <summary>
    /// 注册默认id生成
    /// </summary>
    /// <remarks>
    /// 暂时只支持 Guid, Guid?, long, long?
    /// </remarks>
    public static IServiceCollection AddDefaultIdGenerator(this IServiceCollection services)
    {
        BaseEntity<Guid>.GetKey ??= () => Guid.NewGuid();
        BaseEntity<Guid?>.GetKey ??= () => Guid.NewGuid();
        Entity<Guid>.GetKey ??= () => Guid.NewGuid();
        Entity<Guid?>.GetKey ??= () => Guid.NewGuid();
        BaseEntity<long>.GetKey ??= () => SnowFlake.NextId();
        BaseEntity<long?>.GetKey ??= () => SnowFlake.NextId();
        Entity<long>.GetKey ??= () => SnowFlake.NextId();
        Entity<long?>.GetKey ??= () => SnowFlake.NextId();
        return services;
    }
}
