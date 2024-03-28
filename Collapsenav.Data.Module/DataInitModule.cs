using Collapsenav.Module;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLitePCL;

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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbType = configuration.GetSection("ConnectionStrings")["DbType"];
            if (dbType.NotEmpty())
            {
                Conn conn = dbType.ToLower() switch
                {
                    "sqlite" => new SqliteConn(""),
                };
            }
        }
        services.AddRepository();
    }
    private void Init(IHostBuilder? hostBuilder, IConfiguration? configuration = null)
    {
        if (hostBuilder is null)
            return;
    }
}
