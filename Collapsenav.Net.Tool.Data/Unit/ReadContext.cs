using Microsoft.EntityFrameworkCore;
namespace Collapsenav.Net.Tool.Data;
public abstract class ReadContext : DbContext
{
    public ReadContext(DbContextOptions options) : base(options) { }
}