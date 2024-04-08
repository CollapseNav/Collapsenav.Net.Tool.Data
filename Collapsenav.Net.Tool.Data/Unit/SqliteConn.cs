using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class SqliteConn : Conn
{
    public SqliteConn(string source) : base(source, null, null, null, null)
    {
    }

    public override string ToString()
    {
        return ConnectionString ?? $"Data Source = {Source}";
    }

    public override string GetConnString()
    {
        return ToString();
    }
    public Action<DbContextOptionsBuilder> GetBuilder(Assembly? ass = null)
    {
        return builder => builder.UseSqlite(GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name));
    }
}