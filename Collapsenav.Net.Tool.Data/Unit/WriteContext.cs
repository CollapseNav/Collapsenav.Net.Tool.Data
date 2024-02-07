using Microsoft.EntityFrameworkCore;
namespace Collapsenav.Net.Tool.Data;
public class WriteContext : DbContext
{
    public WriteContext(DbContextOptions options) : base(options) { }

}