using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.Data.Test;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>(new SqliteConn("Test.db"), Assembly.GetExecutingAssembly())
        .AddDefaultDbContext<TestDbContext>()
        .AddRepository()
        .AddRepository(typeof(ModifyRepository<TestEntity>))
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseProvider()
    {
        return new ServiceCollection()
        .AddSqlitePool<TestNotBaseDbContext>(new SqliteConn("Test.db"))
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddRepository()
        .AddRepository(typeof(ModifyRepository<TestEntity>))
        .BuildServiceProvider();
    }
}
