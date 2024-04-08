using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class MysqlConn : Conn
{
    public MysqlConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }
    public override string ToString()
    {
        if (ConnectionString.NotEmpty())
            return ConnectionString;
        StringBuilder sb = new();
        sb.Append($"Server = {Source}; Port = {Port?.ToString() ?? "3306"}; Database = {DataBase}; Uid = {User}; Pwd = {Pwd};");
        return sb.ToString();
    }

    public override string GetConnString()
    {
        return ToString();
    }
    public Action<DbContextOptionsBuilder> GetBuilder(Assembly? ass = null)
    {
#if NETSTANDARD2_0
        return builder => builder.UseMySql(GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name));
#else
        return builder => builder.UseMySql(GetConnString(), ServerVersion.AutoDetect(GetConnString()), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name));
#endif
    }
}