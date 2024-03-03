using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
[Collection("d")]
public class NoConstraintsCrudRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly INoConstraintsCrudRepository<NoConstraintsTestQueryEntity> Repository;
    public NoConstraintsCrudRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<INoConstraintsCrudRepository<NoConstraintsTestQueryEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(31)]
    public async Task NoConstraintsCrudRepositoryQueryTest()
    {
        var datas = await Repository.QueryAsync(item => item.Id < 5);
        Assert.True(datas.Count() == 4);
        var data = await Repository.GetByIdAsync(1);
        Assert.True(data.Number == 92);
        datas = await Repository.QueryAsync(Repository.Query(item => item.Id < 5));
        Assert.True(datas.Count() == 4);
    }

    [Fact, Order(32)]
    public async Task NoConstraintsCrudRepositoryIsExistTest()
    {
        Assert.True(await Repository.IsExistAsync(item => item.Id == 10));
        Assert.True(await Repository.IsExistAsync(item => item.Number <= 2333));
        Assert.True(await Repository.IsExistAsync(item => item.Code.Contains("idie")));
        Assert.False(await Repository.IsExistAsync(item => item.Id == 111));
        Assert.False(await Repository.IsExistAsync(item => item.Number > 2333));
        Assert.False(await Repository.IsExistAsync(item => item.Code == "2333"));
    }

    [Fact, Order(33)]
    public async Task NoConstraintsCrudRepositoryCountTest()
    {
        Assert.True((await Repository.CountAsync(item => item.Id > 4 && item.Id < 9)) == 4);
        Assert.False((await Repository.CountAsync(item => item.Id < 4)) == 4);
    }

    [Fact, Order(34)]
    public async Task NoConstraintsCrudRepositoryQueryByIdsTest()
    {
        var ids = new[] { 1, 3, 5, 7, 9 };
        var data = await Repository.QueryByIdsAsync(ids);
        Assert.True(data.Count() == 5);
        ids = new[] { 2, 6, 8 };
        data = await Repository.QueryByIdsAsync(ids);
        Assert.True(data.Count() == 3);
    }

    [Fact, Order(35)]
    public async Task NoConstraintsCrudRepositoryQueryPageTest()
    {
        var data = await Repository.QueryPageAsync(item => item.Id > 6);
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);

        data = await Repository.QueryPageAsync(Repository.Query(item => item.Id > 6));
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);

        data = await Repository.QueryPageAsync<NoConstraintsTestQueryEntity>(Repository.Query(item => item.Id > 6));
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);
    }
    [Fact, Order(36)]
    public async Task NoConstraintsCrudRepositoryQueryPageOrderTest()
    {
        var data = await Repository.QueryPageAsync(item => true, item => item.Id, true);
        Assert.True(data.Data.Last().Id == 10);
        data = await Repository.QueryPageAsync(item => true, item => item.Id, false);
        Assert.True(data.Data.First().Id == 10);
    }

    [Fact, Order(41)]
    public async Task NoConstraintsCrudRepositoryAddTest()
    {
        var entitys = new List<NoConstraintsTestQueryEntity>{
                new (11,"23333",2333,true),
                new (12,"23333",2333,true),
                new (13,"23333",2333,true),
                new (14,"23333",2333,true),
                new (15,"23333",2333,true),
                new (16,"23333",2333,true),
                new (17,"23333",2333,true),
                new (18,"23333",2333,true),
                new (19,"23333",2333,true),
        };
        await Repository.AddAsync(entitys);
        await Repository.SaveAsync();
        await Repository.AddAsync(new NoConstraintsTestQueryEntity(20, "23333", 2333, true));
        await Repository.SaveAsync();
        var data = await Repository.QueryAsync(item => true);
        Assert.True(data.Count() == 20);
    }

    [Fact, Order(42)]
    public async Task NoConstraintsCrudRepositoryUpdateTest()
    {
        var updateCount = await Repository.UpdateAsync(item => item.Id > 18, entity => new NoConstraintsTestQueryEntity { Number = 123 });
        await Repository.SaveAsync();
        var numberEqual123 = await Repository.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);
        await Repository.UpdateAsync(new NoConstraintsTestQueryEntity { Id = 15, Code = "1111" });
        await Repository.SaveAsync();
        var data = await Repository.GetByIdAsync(15);
        Assert.True(data.Code == "1111");
    }

}
