namespace Collapsenav.Net.Tool.Data;

public class SqliteConn : Conn
{
    public SqliteConn(string source) : base(source, null, null, null, null)
    {
    }

    public override string ToString()
    {
        return $"Data Source = {Source}";
    }

    public override string GetConnString()
    {
        return ToString();
    }
}