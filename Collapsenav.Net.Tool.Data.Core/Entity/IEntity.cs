using System.Reflection;
namespace Collapsenav.Net.Tool.Data;
public interface IEntity
{
    /// <summary>
    /// 统一的实体初始化
    /// </summary>
    void Init();
    /// <summary>
    /// 修改人(并不一定会有)初始化
    /// </summary>
    void InitModify();
    /// <summary>
    /// 软删除/逻辑删除
    /// </summary>
    void SoftDelete();
    /// <summary>
    /// 更新
    /// </summary>
    void Update();
    /// <summary>
    /// 获取主键类型
    /// </summary>
    Type? KeyType();
    /// <summary>
    /// 获取主键属性
    /// </summary>
    PropertyInfo? KeyProp();
}
public interface IEntity<TKey> : IEntity
{
    /// <summary>
    /// 主键
    /// </summary>
    TKey? Id { get; set; }
}