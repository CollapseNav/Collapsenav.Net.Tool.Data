using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data;
public static class DbContextConnExt
{
    public static IServiceCollection AddDbContextPool<T>(this IServiceCollection services, SqliteConn conn, Assembly? ass = null) where T : DbContext
    {
        services.AddSqlitePool<T>(conn, ass);
        return services;
    }
    public static IServiceCollection AddDbContext<T>(this IServiceCollection services, SqliteConn conn, Assembly? ass = null) where T : DbContext
    {
        services.AddSqlite<T>(conn, ass);
        return services;
    }
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, SqliteConn conn, Assembly? ass) where T : DbContext
    {
        return services.AddSqlite<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddSqlite<T>(this IServiceCollection services, string db, Assembly? ass) where T : DbContext
    {
        return services.AddDbContext<T>(options => options.UseSqlite(db.Contains('=') ? db : new SqliteConn(db).GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass.GetName().Name)));
    }
    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, SqliteConn conn, Assembly? ass = null) where T : DbContext
    {
        return services.AddSqlitePool<T>(conn.GetConnString(), ass);
    }
    public static IServiceCollection AddSqlitePool<T>(this IServiceCollection services, string db, Assembly? ass = null) where T : DbContext
    {
        return services.AddDbContextPool<T>(options => options.UseSqlite(db.Contains('=') ? db : new SqliteConn(db).GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass.GetName().Name)));
    }

    /// <summary>
    /// 将 T 注册为默认的 DbContext
    /// </summary>
    public static IServiceCollection AddDefaultDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddScoped<DbContext, T>();
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
        BaseEntity<long>.GetKey ??= () => SnowFlake.NextId();
        BaseEntity<long?>.GetKey ??= () => SnowFlake.NextId();
        return services;
    }

    /// <summary>
    /// 注册读上下文
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddReadContext<Context>(this IServiceCollection services, Action<DbContextOptionsBuilder> options) where Context : ReadContext
    {
        services.AddDbContext<Context>(options);
        services.AddReadContext<Context>();
        return services;
    }
    /// <summary>
    /// 注册写上下文
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddWriteContext<Context>(this IServiceCollection services, Action<DbContextOptionsBuilder> options) where Context : WriteContext
    {
        services.AddDbContext<Context>(options);
        services.AddWriteContext<Context>();
        return services;
    }
    /// <summary>
    /// 注册读上下文
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddReadContext<Context>(this IServiceCollection services) where Context : ReadContext
    {
        services.AddScoped<ReadContext, Context>();
        return services;
    }
    /// <summary>
    /// 注册写上下文
    /// </summary>
    /// <typeparam name="Context"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWriteContext<Context>(this IServiceCollection services) where Context : WriteContext
    {
        services.AddScoped<WriteContext, Context>();
        return services;
    }
}
