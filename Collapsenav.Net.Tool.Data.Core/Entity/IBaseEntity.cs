namespace Collapsenav.Net.Tool.Data;
public interface IBaseEntity : IEntity
{
    /// <summary>
    /// 是否删除
    /// </summary>
    bool? IsDeleted { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime? CreationTime { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}
public interface IBaseEntity<TKey> : IBaseEntity, IEntity<TKey>
{
    /// <summary>
    /// 创建人ID
    /// </summary>
    TKey? CreatorId { get; set; }
    /// <summary>
    /// 最后修改人ID
    /// </summary>
    TKey? LastModifierId { get; set; }
}
