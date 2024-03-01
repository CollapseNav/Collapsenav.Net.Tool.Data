using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Collapsenav.Net.Tool.Data;
public static class RepositoryExt
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
        .AddScoped(typeof(INoConstraintsRepository<>), typeof(NoConstraintsRepository<>))
        .AddScoped(typeof(INoConstraintsRepository<,>), typeof(NoConstraintsRepository<,>))
        .AddScoped(typeof(ICheckExistRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(ICountRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddScoped(typeof(IQueryRepository<,>), typeof(QueryRepository<,>))
        .AddScoped(typeof(INoConstraintsQueryRepository<>), typeof(NoConstraintsQueryRepository<>))
        .AddScoped(typeof(INoConstraintsQueryRepository<,>), typeof(NoConstraintsQueryRepository<,>))
        .AddScoped(typeof(IModifyRepository<>), typeof(ModifyRepository<>))
        .AddScoped(typeof(IModifyRepository<,>), typeof(ModifyRepository<,>))
        .AddScoped(typeof(INoConstraintsModifyRepository<>), typeof(NoConstraintsModifyRepository<>))
        .AddScoped(typeof(INoConstraintsModifyRepository<,>), typeof(NoConstraintsModifyRepository<,>))
        .AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>))
        .AddScoped(typeof(ICrudRepository<,>), typeof(CrudRepository<,>))
        .AddScoped(typeof(ICrudRepository<,,>), typeof(CrudRepository<,,>))
        .AddScoped(typeof(INoConstraintsCrudRepository<>), typeof(NoConstraintsCrudRepository<>))
        .AddScoped(typeof(INoConstraintsCrudRepository<,>), typeof(NoConstraintsCrudRepository<,>))
        .AddScoped(typeof(INoConstraintsCrudRepository<,,>), typeof(NoConstraintsCrudRepository<,,>))
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
}
