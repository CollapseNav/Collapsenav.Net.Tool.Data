namespace Collapsenav.Data.Module;

public class ConnectNode
{
    public ConnectNode()
    {
        ConnectionString = string.Empty;
    }

    public string ConnectionString { get; set; }
    public ConnectionType? DbType { get; set; }
}


public enum ConnectionType
{
    Mssql,
    MySql,
    MariaDb,
    Sqlite,
    Pgsql
}