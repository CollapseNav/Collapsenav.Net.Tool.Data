using Collapsenav.Module;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
builder.Services.LoadModules(builder.Host, builder.Configuration, builder.Environment);
var app = builder.Build();
app.UseAutoCommit();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
