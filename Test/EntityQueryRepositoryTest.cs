using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
[Collection("1")]
public class EntityQueryRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IEntityQueryRepository<TestEntity> Repository;
    public EntityQueryRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IEntityQueryRepository<TestEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }


    // [Fact, Order(12)]
    // public async Task QueryRepositoryIsExistTest()
    // {
    //     Assert.True(await Repository.IsExistAsync(item => item.Id == 10));
    //     Assert.True(await Repository.IsExistAsync(item => item.Number <= 2333));
    //     Assert.True(await Repository.IsExistAsync(item => item.Code.Contains("yxm")));
    //     Assert.False(await Repository.IsExistAsync(item => item.Id == 111));
    //     Assert.False(await Repository.IsExistAsync(item => item.Number > 2333));
    //     Assert.False(await Repository.IsExistAsync(item => item.Code == "2333"));
    // }

    // [Fact, Order(13)]
    // public async Task QueryRepositoryCountTest()
    // {
    //     Assert.True((await Repository.CountAsync(item => item.Id > 4 && item.Id < 9)) == 4);
    //     Assert.False((await Repository.CountAsync(item => item.Id < 4)) == 4);
    // }

    [Fact, Order(14)]
    public async Task QueryRepositoryQueryByIdsTest()
    {
        var ids = new[] { 1, 3, 5, 7, 9 };
        var data = await Repository.QueryByIdsAsync(ids);
        Assert.True(data.Count() == 5);
        ids = new[] { 2, 6, 8, 1000 };
        data = await Repository.QueryByIdsAsync(ids);
        ids = null;
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await Repository.QueryByIdsAsync(ids));

        int? i = null;
        Assert.Null(await Repository.GetByIdAsync(i));
        Assert.True(data.Count() == 3);
        data = await Repository.QueryByIdsAsync(Enumerable.Empty<int>());
        Assert.True(data.IsEmpty());
    }

    [Fact, Order(15)]
    public async Task QueryRepositoryQueryPageTest()
    {
        var data = await Repository.QueryPageAsync(item => item.Id > 6);
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);

        data = await Repository.QueryPageAsync(i => i.Id > 6);
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);

        // data = await Repository.QueryPageAsync<TestEntity>(i => i.Id > 6, new PageRequest());
        // Assert.True(data.Length == 4);
        // Assert.True(data.Data.First().Id == 7);
        // Assert.True(data.Data.Last().Id == 10);
    }
    [Fact, Order(16)]
    public async Task QueryRepositoryQueryPageOrderTest()
    {
        var data = await Repository.QueryPageAsync(item => true, item => item.Id, true);
        Assert.True(data.Data.Last().Id == 10);
        data = await Repository.QueryPageAsync(item => true, item => item.Id, false);
        Assert.True(data.Data.First().Id == 10);
    }
}
