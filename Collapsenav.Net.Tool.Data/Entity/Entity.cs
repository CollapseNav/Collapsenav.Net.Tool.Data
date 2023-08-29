using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Collapsenav.Net.Tool.Data;
public abstract class Entity : IEntity
{
    public virtual void Init()
    {
        InitModify();
    }
    public virtual void InitModify() { }

    public virtual PropertyInfo? KeyProp()
    {
        var prop = GetType().AttrValues<KeyAttribute>().FirstOrDefault().Key;
        return prop;
    }

    public virtual Type? KeyType()
    {
        return KeyProp()?.PropertyType;
    }

    public virtual void SoftDelete()
    {
        InitModify();
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

public abstract class AutoIncrementEntity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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