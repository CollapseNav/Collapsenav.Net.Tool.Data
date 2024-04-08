using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class PgsqlConn : Conn
{
    public PgsqlConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }

    public override string ToString()
    {
        if (ConnectionString.NotEmpty())
            return ConnectionString;
        StringBuilder sb = new();
        sb.Append($"Host = {Source}; Port = {Port.ToString()}; Database = {DataBase}; Username = {User}; Password = {Pwd};");
        return sb.ToString();
    }

    public override string GetConnString()
    {
        return ToString();
    }
    public Action<DbContextOptionsBuilder> GetBuilder(Assembly? ass = null)
    {
        return builder => builder.UseNpgsql(GetConnString(), ass == null ? null : m => m.MigrationsAssembly(ass?.GetName().Name));
    }
}