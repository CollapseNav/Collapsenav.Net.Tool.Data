using Collapsenav.Module;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IDB, EFDB<EntityContext>>();
builder.Services.AddEndpointsApiExplorer();
// SqliteConn conn = new SqliteConn("Data Source=Data.db");
// builder.Services.AddDbContext<EntityContext>(conn.GetBuilder());
// builder.Services.AddScoped(typeof(DbContext), typeof(EntityContext));
builder.Services.AddDefaultSwaggerGen();
builder.Services.LoadModules(builder.Host, builder.Configuration, builder.Environment);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
