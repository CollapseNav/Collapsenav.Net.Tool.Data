using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collapsenav.Net.Tool.Data;

public abstract class BaseEntity : Entity, IBaseEntity
{
    public bool? IsDeleted { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <Remarks>
    /// pgsql要求存入的为utc时间, 此时就可以通过重新赋值进行修正
    /// </Remarks>
    public static Func<DateTime> GetNow { get; set; } = () => DateTime.Now;

    public override void Init()
    {
        CreationTime = GetNow();
        base.Init();
    }

    public override void InitModify()
    {
        LastModificationTime = GetNow();
        base.InitModify();
    }
}
public abstract class BaseEntity<TKey> : BaseEntity, IBaseEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public TKey? Id { get; set; }
    public TKey? CreatorId { get; set; }
    public TKey? LastModifierId { get; set; }
    public override void Init()
    {
        if (GetKey != null)
            Id = GetKey();
        IsDeleted = false;
        base.Init();
    }
    /// <summary>
    /// 获取主键值
    /// </summary>
    public static Func<TKey>? GetKey { get; set; } = null;

    public override void SoftDelete()
    {
        IsDeleted = true;
        base.SoftDelete();
    }

    public override void InitModify()
    {
        LastModificationTime = GetNow();
        base.InitModify();
    }

    public override Type? KeyType()
    {
        return typeof(TKey);
    }
}

public abstract class AutoIncrementBaseEntity<TKey> : BaseEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new TKey? Id { get; set; }
    public override void Init()
    {
        CreationTime = GetNow();
        LastModificationTime = GetNow();
        IsDeleted = false;
        base.Init();
    }
}