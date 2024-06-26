using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using DataDemo.EntityLib;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetDbDemo.Controllers;

public class SecondInput
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
[ApiController]
[Route("[controller]")]
public class SecondController : ControllerBase
{
    private readonly ICrudRepository<SecondEntity> _repository;
    public SecondController(ICrudRepository<ReadContext, SecondEntity> repository)
    {
        _repository = repository;
    }

    // [HttpPost]
    // public async Task<SecondEntity> CreateEntity(SecondEntity input)
    // {
    //     input = await _repository.AddAsync(input);
    //     await _repository.SaveAsync();
    //     return input;
    // }
    // [HttpGet("{id}")]
    // public async Task<SecondEntity> GetOne(long? id)
    // {
    //     return await _repository.GetByIdAsync(id);
    // }
    [HttpGet]
    public async Task<IEnumerable<SecondEntity>> GetList([FromQuery] SecondInput input)
    {
        return await _repository.Query(item => true)
        .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
        .WhereIf(input.Name.NotEmpty(), item => item.Name == input.Name)
        .WhereIf(input.Description.NotEmpty(), item => item.Description == input.Description)
        .ToListAsync();
    }
    // [HttpPut]
    // public async Task<int> UpdateEntity(SecondEntity input)
    // {
    //     var count = await _repository.UpdateAsync(input);
    //     await _repository.SaveAsync();
    //     return count;
    // }

    // [HttpDelete("{id}")]
    // public async Task<bool> DeleteEntity(long id, [FromQuery] bool trueDel = true)
    // {
    //     var flag = await _repository.DeleteAsync(id, trueDel);
    //     await _repository.SaveAsync();
    //     return flag;
    // }
}
