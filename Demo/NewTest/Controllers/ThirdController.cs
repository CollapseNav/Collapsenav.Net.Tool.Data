using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using DataDemo.EntityLib;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetDbDemo.Controllers;

public class ThirdInput
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
[ApiController]
[Route("[controller]")]
public class ThirdController : ControllerBase
{
    private readonly ICrudRepository<ThirdEntity> _repository;
    public ThirdController(ICrudRepository<ThirdEntity> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<ThirdEntity>> GetList([FromQuery] ThirdInput input)
    {
        return await _repository.Query(item => true)
        .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
        .WhereIf(input.Name.NotEmpty(), item => item.Name == input.Name)
        .WhereIf(input.Description.NotEmpty(), item => item.Description == input.Description)
        .ToListAsync();
    }

    [HttpPost("Create")]
    public async Task<ThirdEntity?> GetById([FromBody] ThirdEntity input)
    {
        var entity = await _repository.AddAsync(input);
        await _repository.SaveAsync();
        return entity;
    }
}
