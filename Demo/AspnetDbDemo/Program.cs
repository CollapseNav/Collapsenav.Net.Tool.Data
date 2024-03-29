using AspnetDbDemo;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var mariadbConn = new SqliteConn("Data.db");
builder.Services.AddDbContext<EntityContext>(mariadbConn);
// builder.Services.AddSqlitePool<EntityContext>("./Data.db");
builder.Services.AddReadContext<RRContext>(mariadbConn.GetBuilder());
builder.Services.AddWriteContext<WWContext>(mariadbConn.GetBuilder());
builder.Services.AddDefaultDbContext<EntityContext>();
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddRepository();
var app = builder.Build();
app.UseAutoCommit();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
