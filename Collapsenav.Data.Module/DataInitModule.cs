using System.Reflection;
using Collapsenav.Module;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Collapsenav.Data.Module;

public class DataInitModule : InitModule
{
    public virtual void Init(IServiceCollection services, IHostBuilder? hostBuilder = null, IConfiguration? configuration = null, IHostEnvironment? environment = null)
    {
        Init(services, configuration);
        Init(hostBuilder, configuration);
    }
    private void Init(IServiceCollection services, IConfiguration? configuration = null)
    {
        if (configuration is not null)
        {
            var connection = GetConnection(configuration);
            var connTypes = AppDomain.CurrentDomain.GetTypes<Conn>().Where(i => !i.IsAbstract).ToList();

            if (connection.ConnectionString.NotEmpty() && connection.DbType != null)
            {
                var connType = connTypes.FirstOrDefault(i => i.Name.StartsWith(connection.DbType.ToString()!, StringComparison.OrdinalIgnoreCase));
                var dbcontexts = AppDomain.CurrentDomain.GetTypes<DbContext>().ToArray();
                if (connType is not null)
                {
                    Conn conn = (Activator.CreateInstance(connType, "") as Conn)!;
                    conn.LoadConnectString(connection.ConnectionString);
                    var action = (Action<DbContextOptionsBuilder>)connType.GetMethod("GetBuilder")?.Invoke(conn, new object?[] { null })!;
                    InitDbContext(services, action, dbcontexts);
                }
            }
        }
        services.AddRepository();
    }
    private void Init(IHostBuilder? hostBuilder, IConfiguration? configuration = null)
    {
        if (hostBuilder is null)
            return;
    }
    /// <summary>
    /// 初始化DbContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <param name="contextTypes"></param>
    private void InitDbContext(IServiceCollection services, Action<DbContextOptionsBuilder> action, Type[] contextTypes)
    {
        contextTypes = contextTypes.Where(type => !type.IsAbstract && type != typeof(DbContext)).ToArray();
        foreach (var dbcontext in contextTypes)
        {
            MethodInfo[]? methods = null;
            // 尝试注册 WriteContext
            if (dbcontext.IsType<WriteContext>())
            {
                methods = typeof(DbContextConnExt).GetMethods();
                var addmethod = methods.First(i => i.Name == nameof(DbContextConnExt.AddWriteContext) && i.IsGenericMethod && i.GetParameters().Length == 2);
                addmethod.MakeGenericMethod(dbcontext).Invoke(services, new object?[] { services, action });
            }
            // 尝试注册 ReadContext
            else if (dbcontext.IsType<ReadContext>())
            {
                methods = typeof(DbContextConnExt).GetMethods();
                var addmethod = methods.First(i => i.Name == nameof(DbContextConnExt.AddReadContext) && i.IsGenericMethod && i.GetParameters().Length == 2);
                addmethod.MakeGenericMethod(dbcontext).Invoke(services, new object?[] { services, action });
            }
            // 尝试注册普通DbContext
            else
            {
                methods = typeof(EntityFrameworkServiceCollectionExtensions).GetMethods();
                var addmethod = methods.First(i => i.Name == nameof(EntityFrameworkServiceCollectionExtensions.AddDbContext) && i.IsGenericMethod && i.GetParameters().Length > 2 && i.GetParameters()[2].ParameterType == typeof(ServiceLifetime));
                addmethod.MakeGenericMethod(dbcontext).Invoke(services, new object?[] { services, action, null, null });
                services.AddScoped(typeof(DbContext), dbcontext);
            }
        }
    }
    /// TODO: 可能需要调整json配置的格式，使其支持配置读写分离
    /// <summary>
    /// 尝试从appsettings.json中获取数据库连接
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private ConnectNode GetConnection(IConfiguration? configuration = null)
    {
        if (configuration is null)
            return new();
        var conn = new ConnectNode();
        var connectionSection = configuration.GetSection("Connection");
        if (connectionSection.Exists())
        {
            var children = connectionSection.GetChildren();
            if (children.Any(i => i.Key == nameof(ConnectNode.ConnectionString)))
                conn.ConnectionString = connectionSection[nameof(ConnectNode.ConnectionString)] ?? string.Empty;

            if (children.Any(i => i.Key == nameof(ConnectNode.DbType)))
                conn.DbType = (ConnectionType)Enum.Parse(typeof(ConnectionType), connectionSection[nameof(ConnectNode.DbType)] ?? "Sqlite", true);
        }
        return conn;
    }

    public virtual void Use(IApplicationBuilder app, IConfiguration? configuration = null, IHostEnvironment? environment = null)
    {
        app.UseAutoCommit();
    }
}
