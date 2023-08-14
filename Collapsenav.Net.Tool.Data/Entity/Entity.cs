using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Collapsenav.Net.Tool.Data;
public partial class Entity : IEntity
{
    public virtual void Init()
    {
        InitModifyId();
    }
    public virtual void InitModifyId()
    {
    }

    public PropertyInfo? KeyProp()
    {
        var prop = GetType().AttrValues<KeyAttribute>().FirstOrDefault().Key;
        return prop;
    }

    public Type? KeyType()
    {
        return KeyProp()?.PropertyType;
    }

    public virtual void SoftDelete()
    {
        InitModifyId();
    }
    public virtual void Update()
    {
        InitModifyId();
    }
}
public partial class Entity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public TKey? Id { get; set; }
}


public partial class AutoIncrementEntity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey? Id { get; set; }
}