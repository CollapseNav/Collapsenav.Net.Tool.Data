using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
[Collection("c")]
public class NoConstraintsModifyRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly INoConstraintsModifyRepository<NoConstraintsTestModifyEntity> Repository;
    protected readonly INoConstraintsQueryRepository<NoConstraintsTestModifyEntity> Read;
    public NoConstraintsModifyRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<INoConstraintsModifyRepository<NoConstraintsTestModifyEntity>>();
        Read = GetService<INoConstraintsQueryRepository<NoConstraintsTestModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(21)]
    public async Task NoConstraintsModifyRepositoryAddTest()
    {
        var entitys = new List<NoConstraintsTestModifyEntity>{
                new (11,"23333",2333,true),
                new (12,"23333",2333,true),
                new (13,"23333",2333,true),
                new (14,"23333",2333,true),
                new (15,"23333",2333,true),
                new (16,"23333",2333,true),
                new (17,"23333",2333,true),
                new (18,"23333",2333,true),
                new (19,"23333",2333,true),
                new (20,"23333",2333,true),
            };
        await Repository.AddAsync(entitys.First());
        NoConstraintsTestModifyEntity a = null;
        Assert.Null(await Repository.AddAsync(a));
        await Repository.AddAsync(entitys.Skip(1));
        IEnumerable<NoConstraintsTestModifyEntity> nullValue = null;
        await Repository.AddAsync(nullValue);
        await Repository.SaveAsync();
        var data = await Read.QueryAsync(item => true);
        Assert.True(data.Count() == 20);
    }

    [Fact, Order(22)]
    public async Task NoConstraintsModifyRepositoryUpdateTest()
    {
        var updateCount = await Repository.UpdateAsync(item => item.Id > 18, entity => new NoConstraintsTestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        var numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);
        updateCount = await Repository.UpdateWithoutTransactionAsync(item => item.Id > 18, entity => new NoConstraintsTestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);

        updateCount = await Repository.UpdateWithoutTransactionAsync(null, entity => new NoConstraintsTestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 0);

        updateCount = await Repository.UpdateAsync(null, entity => new NoConstraintsTestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 0);
    }

    [Fact, Order(24)]
    public async Task NoConstraintsModifyRepositoryDeleteTest()
    {
        var delCount = await Repository.DeleteAsync(item => item.Id < 11, true);
        await Repository.SaveAsync();
        Assert.True(delCount == 10);
        await Repository.DeleteAsync(11, true);
        Repository.Save();
        await Repository.DeleteByIdsAsync(new[] { 12 }, true);
        Repository.Save();
        var leftData = await Read.QueryAsync(item => true);
        Assert.True(leftData.Count() == 8);
    }


    [Fact, Order(25)]
    public async Task NoConstraintsModifyRepositoryDeleteAllTest()
    {
        var delCount = await Repository.DeleteAsync(item => true, true);
        await Repository.SaveAsync();
        var leftData = await Read.QueryAsync(item => true);
        Assert.True(delCount == 8);
        Assert.True(leftData.IsEmpty());
    }

    [Fact, Order(26)]
    public async Task NoConstraintsModifyRepositoryAutoSaveRollBackTestAsync()
    {
        var repo = GetService<INoConstraintsModifyRepository<NoConstraintsTestModifyEntity>>();
        await repo.AddAsync(new NoConstraintsTestModifyEntity());
        repo.Dispose();
        repo = GetService<INoConstraintsModifyRepository<NoConstraintsTestModifyEntity>>();
        var query = await repo.Query().ToListAsync();
        Assert.Empty(query);
    }
    [Fact, Order(26)]
    public async Task NoConstraintsModifyRepositoryAutoSaveTestAsync()
    {
        TransManager.UseAutoCommit();
        INoConstraintsModifyRepository<NoConstraintsTestModifyEntity> repo = new NoConstraintsModifyRepository<NoConstraintsTestModifyEntity>(GetService<TestDbContext>());
        await repo.AddAsync(new NoConstraintsTestModifyEntity());
        repo.Dispose();
        repo = new NoConstraintsModifyRepository<NoConstraintsTestModifyEntity>(GetService<TestDbContext>());
        var query = await repo.Query().ToListAsync();
        await repo.DeleteAsync(item => true, true);
        repo.Dispose();
        Assert.NotEmpty(query);
        TransManager.UseAutoCommit(false);
    }
}
