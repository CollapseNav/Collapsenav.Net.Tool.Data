using Collapsenav.Module;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Collapsenav.Data.Module;

public class DataInitModule : InitModule
{
    public override void Init(IServiceCollection services, IHostBuilder? hostBuilder = null, IConfiguration? configuration = null, IHostEnvironment? environment = null)
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
                var dbcontexts = AppDomain.CurrentDomain.GetTypes<DbContext>().Where(i => !i.IsAbstract).ToList();
                if (connType is not null)
                {
                    Conn conn = (Activator.CreateInstance(connType, "") as Conn)!;
                    conn.LoadConnectString(connection.ConnectionString);
                    var obj = (Action<DbContextOptionsBuilder>)connType.GetMethod("GetBuilder")?.Invoke(conn, new object?[] { null })!;
                    foreach (var dbcontext in dbcontexts)
                    {
                        if (!dbcontext.IsType<WriteContext>() && !dbcontext.IsType<ReadContext>() && dbcontext != typeof(DbContext))
                        {
                            var ms = typeof(EntityFrameworkServiceCollectionExtensions).GetMethods();
                            var addmethod = ms.First(i => i.IsGenericMethod && i.GetParameters().Length > 2 && i.GetParameters()[2].ParameterType == typeof(ServiceLifetime));
                            addmethod.MakeGenericMethod(dbcontext).Invoke(services, new object?[] { services, obj, null, null });
                            services.AddScoped(typeof(DbContext), dbcontext);
                        }
                    }
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
}
