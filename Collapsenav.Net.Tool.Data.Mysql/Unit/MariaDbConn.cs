using System.Text;

namespace Collapsenav.Net.Tool.Data;

public class MariaDbConn : Conn
{
    public MariaDbConn(string source, int? port, string dataBase, string user, string pwd) : base(source, port, dataBase, user, pwd)
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
}