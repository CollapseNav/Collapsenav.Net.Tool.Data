using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using DataDemo.EntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetDbDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class FirstController : ControllerBase
{
    // public FirstController(IDB idb)
    // {
    // }

    private ICrudRepository<FirstEntity> _repository;

    public FirstController(ICrudRepository<FirstEntity> repository)
    // public FirstController(EntityContext entity)
    {
        _repository = repository;
    }

    [HttpPost("Create")]
    public async Task<FirstEntity> CreateEntity(FirstEntity input)
    {
        await _repository.AddAsync(input);
        await _repository.SaveAsync();
        return input;
    }
    [HttpPost("Update")]
    public async Task<IEnumerable<FirstEntity>> UpdateEntity(IEnumerable<FirstEntity> input)
    {
        await _repository.UpdateAsync(item => item.Name == "string", item => new() { Description = "2333" });
        await _repository.SaveAsync();
        return input;
    }
    [HttpGet("GetAll")]
    public async Task<IEnumerable<FirstEntity>> GetFirstEntitiesAsync()
    {
        return await _repository.QueryAsync();
    }
    [HttpDelete("DeleteById")]
    public async Task<int> DeleteById([FromQuery] long? id, [FromQuery] bool isTrue = false)
    {
        var result = await _repository.DeleteAsync(id, isTrue);
        await _repository.SaveAsync();
        return result;
    }
    [HttpDelete("DeleteByIds")]
    public async Task<int> DeleteByIds([FromBody] long[] ids)
    {
        var result = await _repository.DeleteByIdsAsync(ids, true);
        await _repository.SaveAsync();
        return result;
    }
    // [HttpGet]
    // public async Task<IEnumerable<FirstEntity>> GetList([FromQuery] FirstInput input)
    // {
    //     // await _repository.AddAsync(new FirstEntity());
    //     // throw new Exception();
    //     // await threpo.AddAsync(new ThirdEntity());
    //     // return await _repository.Query(item => true)
    //     // .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
    //     // .WhereIf(input.Name.NotEmpty(), item => item.Name == input.Name)
    //     // .WhereIf(input.Description.NotEmpty(), item => item.Description == input.Description)
    //     // .ToListAsync();
    //     return await _repository.QueryAsync(i => true);
    // }

    // [HttpPut]
    // public async Task<int> UpdateEntity(FirstEntity input)
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
