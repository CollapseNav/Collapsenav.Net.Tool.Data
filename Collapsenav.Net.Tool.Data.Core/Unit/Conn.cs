namespace Collapsenav.Net.Tool.Data;

/// <summary>
/// 基础数据库连接配置
/// </summary>
public abstract class Conn
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">主机</param>
    /// <param name="port">端口</param>
    /// <param name="dataBase">数据库</param>
    /// <param name="user">用户</param>
    /// <param name="pwd">密码</param>
    protected Conn(string? source, int? port, string? dataBase, string? user, string? pwd)
    {
        Source = source;
        Port = port;
        DataBase = dataBase;
        User = user;
        Pwd = pwd;
        ConnectionString = null;
    }

    /// <summary>
    /// 获取连接字符串
    /// </summary>
    /// <returns></returns>
    public abstract string GetConnString();
    public void LoadConnectString(string connString)
    {
        ConnectionString = connString;
    }
    public string? ConnectionString { get; protected set; }
    /// <summary>
    /// 主机
    /// </summary>
    /// <value></value>
    public string? Source { get; set; }
    /// <summary>
    /// 端口
    /// </summary>
    /// <value></value>
    public int? Port { get; set; }
    /// <summary>
    /// 数据库名称
    /// </summary>
    /// <value></value>
    public string? DataBase { get; set; }
    /// <summary>
    /// 用户
    /// </summary>
    /// <value></value>
    public string? User { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    /// <value></value>
    public string? Pwd { get; set; }
}