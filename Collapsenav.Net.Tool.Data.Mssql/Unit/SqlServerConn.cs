using System.Text;

namespace Collapsenav.Net.Tool.Data;

public class SqlServerConn : Conn
{
    public SqlServerConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
    {
    }

    public override string ToString()
    {
        return GetConnString();
    }

    public override string GetConnString()
    {
        StringBuilder sb = new();
        sb.Append($"Data Source = {Source},{Port?.ToString() ?? "1433"}; Database = {DataBase}; Uid = {User}; Pwd = {Pwd};");
        return sb.ToString();
    }
}