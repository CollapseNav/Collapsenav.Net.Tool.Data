using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using DataDemo.EntityLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetDbDemo.Controllers;

public class FirstInput
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
[ApiController]
[Route("[controller]")]
public class FirstController : ControllerBase
{
    private readonly ICrudRepository<FirstEntity> _repository;
    private readonly IModifyRepository<SecondEntity> secRepo;
    private readonly IQueryRepository<RRContext, FirstEntity> readrepo;
    private readonly INoConstraintsCrudRepository<NoConstraintsThirdEntity> nothird;

    public FirstController(ICrudRepository<FirstEntity> repository, IModifyRepository<WriteContext, SecondEntity> secRepo, IQueryRepository<RRContext, FirstEntity> readrepo, INoConstraintsCrudRepository<NoConstraintsThirdEntity> nothird)
    {
        _repository = repository;
        this.secRepo = secRepo;
        this.readrepo = readrepo;
        this.nothird = nothird;
    }

    [HttpPost]
    public async Task<FirstEntity> CreateEntity(FirstEntity input)
    {
        input.Name = DateTime.Now.ToDefaultTimeString();
        input = await _repository.AddAsync(input);
        await nothird.AddAsync(new NoConstraintsThirdEntity() { Name = DateTime.Now.ToDefaultMilliString() });
        return input;
    }
    [HttpGet("{id}")]
    public async Task<FirstEntity> GetOne(long? id)
    {
        return await readrepo.GetByIdAsync(id);
    }
    [HttpGet]
    public async Task<IEnumerable<FirstEntity>> GetList([FromQuery] FirstInput input)
    {
        var data = await nothird.QueryAsync();
        // await _repository.AddAsync(new FirstEntity());
        await secRepo.AddAsync(new SecondEntity() { Id = (int)SnowFlake.NextId() });
        // throw new Exception();
        // await threpo.AddAsync(new ThirdEntity());
        // return await _repository.Query(item => true)
        // .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
        // .WhereIf(input.Name.NotEmpty(), item => item.Name == input.Name)
        // .WhereIf(input.Description.NotEmpty(), item => item.Description == input.Description)
        // .ToListAsync();
        return null;
    }

    [HttpPut]
    public async Task<int> UpdateEntity(FirstEntity input)
    {
        var count = await _repository.UpdateAsync(input);
        await _repository.SaveAsync();
        return count;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteEntity(long id, [FromQuery] bool trueDel = true)
    {
        var flag = await _repository.DeleteAsync(id, trueDel);
        await _repository.SaveAsync();
        return flag;
    }
}
