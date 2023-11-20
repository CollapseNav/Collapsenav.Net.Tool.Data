using Collapsenav.Net.Tool.Data;

namespace DataDemo.EntityLib;

/// <summary>
/// 实体1
/// </summary>
public class FirstEntity : Entity<long>
{
    public string Name { get; set; }
    public string Description { get; set; }
}