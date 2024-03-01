using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;

namespace DataDemo.EntityLib;

public class EntityContext : DbContext
{
    public DbSet<FirstEntity> FirstEntity { get; set; }
    public DbSet<SecondEntity> SecondEntity { get; set; }
    // public DbSet<ThirdEntity> ThirdEntity { get; set; }
    public DbSet<NoConstraintsThirdEntity> NoThirdEntity { get; set; }
    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }
}


public class WWContext : WriteContext
{
    public DbSet<SecondEntity> SecondEntity { get; set; }
    public WWContext(DbContextOptions options) : base(options)
    {
    }
}
public class RRContext : ReadContext
{
    public DbSet<FirstEntity> FirstEntity { get; set; }
    public RRContext(DbContextOptions<RRContext> options) : base(options)
    {
    }
}