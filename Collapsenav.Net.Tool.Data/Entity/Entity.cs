using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Collapsenav.Net.Tool.Data;
public abstract class Entity : IEntity
{
    public virtual void Init()
    {
        Update();
    }
    public virtual void InitModify() { }

    public virtual PropertyInfo? KeyProp()
    {
        // 默认情况下尝试将有KeyAttribute的属性作为主键
        var prop = GetType().AttrValues<KeyAttribute>().FirstOrDefault().Key;
        return prop;
    }

    public virtual Type? KeyType()
    {
        return KeyProp()?.PropertyType;
    }

    public virtual void SoftDelete()
    {
        Update();
    }
    public virtual void Update()
    {
        InitModify();
    }
}
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public TKey? Id { get; set; }
    public override PropertyInfo? KeyProp()
    {
        return GetType().GetProperty("Id");
    }
    public override Type? KeyType()
    {
        return typeof(TKey);
    }
}

public abstract class AutoIncrementEntity<TKey> : Entity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new TKey? Id { get; set; }
}