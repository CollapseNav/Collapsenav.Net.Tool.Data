using Collapsenav.Net.Tool.Data;

namespace DataDemo.EntityLib;

/// <summary>
/// 实体2
/// </summary>
public class SecondEntity : BaseEntity<int?>
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}