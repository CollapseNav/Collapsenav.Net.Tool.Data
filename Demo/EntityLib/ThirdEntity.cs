using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Collapsenav.Net.Tool.Data;

namespace DataDemo.EntityLib;

/// <summary>
/// 实体3
/// </summary>
public class ThirdEntity : BaseEntity<long?>
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}

[Table("ThirdEntity")]
public class NoConstraintsThirdEntity
{
    [Key]
    public long? Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}