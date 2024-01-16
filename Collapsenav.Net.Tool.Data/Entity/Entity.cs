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
    protected PropertyInfo? keyProp;
    public virtual PropertyInfo? KeyProp()
    {
        // 默认情况下尝试将有KeyAttribute的属性作为主键
        keyProp ??= GetType().AttrValues<KeyAttribute>().FirstOrDefault().Key;
        return keyProp;
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
    public virtual void SetKeyValue(object input)
    {
        KeyProp()?.SetValue(this, ConvertKeyValue(input));
    }

    public virtual object ConvertKeyValue(object input)
    {
        var keyType = KeyType();

        if (keyType != null && keyType.IsGenericType)
            keyType = keyType.GenericTypeArguments.First();

        var obj = keyType!.Name switch
        {
            nameof(Int32) => int.Parse(input.ToString() ?? string.Empty),
            nameof(Int64) => long.Parse(input.ToString() ?? string.Empty),
            nameof(String) => input.ToString() ?? string.Empty,
            nameof(Guid) => Guid.Parse(input.ToString() ?? string.Empty),
            _ => keyType.HasMethod("Parse") ? (keyType.GetMethod("Parse")!.Invoke(null, new[] { input }) ?? 0) : 0,
        };
        return obj;
    }
}
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public TKey? Id { get; set; }
    public override PropertyInfo? KeyProp()
    {
        keyProp ??= GetType().GetProperty("Id");
        return keyProp;
    }
    public override Type? KeyType()
    {
        return typeof(TKey);
    }

    public override void SetKeyValue(object input)
    {
        if (input.GetType().IsType(typeof(TKey)))
            Id = (TKey)input;
        else
            KeyProp()?.SetValue(this, ConvertKeyValue(input));
    }
}
public abstract class AutoIncrementEntity<TKey> : Entity, IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey? Id { get; set; }
    public override PropertyInfo? KeyProp()
    {
        keyProp ??= GetType().GetProperty("Id");
        return keyProp;
    }
    public override Type? KeyType()
    {
        return typeof(TKey);
    }

    public override void SetKeyValue(object input)
    {
        if (input.GetType().IsType(typeof(TKey)))
            Id = (TKey)input;
        else
            KeyProp()?.SetValue(this, ConvertKeyValue(input));
    }
}