using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class SqlServerConn : Conn
{
    public SqlServerConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }

    public override string ToString()
    {
        if (ConnectionString.NotEmpty())
            return ConnectionString;
        StringBuilder sb = new();
        sb.Append($"Data Source = {Source},{Port?.ToString() ?? "1433"}; Database = {DataBase}; Uid = {User}; Pwd = {Pwd};");
        return sb.ToString();
    }

    public override string GetConnString()
    {
        return ToString();
    }
    public Action<DbContextOptionsBuilder> GetBuilder(Assembly? ass = null)
    {
        return builder => builder.UseSqlServer(GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name));
    }
}