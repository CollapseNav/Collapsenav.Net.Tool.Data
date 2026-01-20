using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Collapsenav.Net.Tool.Data;

public static class RepositoryExt
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped(typeof(INoConstraintsRepository<>), typeof(NoConstraintsRepository<>))
        .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(INoConstraintsQueryRepository<>), typeof(NoConstraintsQueryRepository<>))
        .AddScoped(typeof(IModifyRepository<>), typeof(ModifyRepository<>))
        .AddScoped(typeof(INoConstraintsModifyRepository<>), typeof(NoConstraintsModifyRepository<>))
        .AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>))
        .AddScoped(typeof(INoConstraintsCrudRepository<>), typeof(NoConstraintsCrudRepository<>))
        .AddDefaultIdGenerator()
        ;
        return services;
    }
    public static IServiceCollection AddRepository(this IServiceCollection services, params Type[] types)
    {
        services.AddRepository();
        foreach (var type in types)
            services.TryAddScoped(type);
        return services;
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
