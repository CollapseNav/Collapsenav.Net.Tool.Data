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
public class ModifyRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IModifyRepository<TestModifyEntity> Repository;
    protected readonly IQueryRepository<TestModifyEntity> Read;
    public ModifyRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IModifyRepository<TestModifyEntity>>();
        Read = GetService<IQueryRepository<TestModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(21)]
    public async Task ModifyRepositoryAddTest()
    {
        var entitys = new List<TestModifyEntity>{
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
        await Repository.AddAsync(entitys.Skip(1));
        IEnumerable<TestModifyEntity> nullValue = null;
        await Repository.AddAsync(nullValue);
        await Repository.SaveAsync();
        var data = await Read.QueryAsync(item => true);
        Assert.True(data.Count() == 20);
    }

    [Fact, Order(22)]
    public async Task ModifyRepositoryUpdateTest()
    {
        var updateCount = await Repository.UpdateAsync(item => item.Id > 18, entity => new TestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        var numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);
        updateCount = await Repository.UpdateWithoutTransactionAsync(item => item.Id > 18, entity => new TestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);

        updateCount = await Repository.UpdateWithoutTransactionAsync(null, entity => new TestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 0);

        updateCount = await Repository.UpdateAsync(null, entity => new TestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 0);
    }

    [Fact, Order(23)]
    public async Task ModifyRepositorySoftDeleteTest()
    {
        var delCount = await Repository.DeleteAsync(item => item.Id < 11, false);
        Assert.Equal(0, await Repository.DeleteAsync(null, false));
        await Repository.SaveAsync();
        Assert.True(delCount == 10);
        await Repository.DeleteAsync(11, false);
        int? value = null;
        await Repository.DeleteAsync(value, false);
        await Repository.DeleteAsync(10000, false);
        Repository.Save();
        await Repository.DeleteByIdsAsync(new[] { 12 }, false);
        IEnumerable<int> nullList = null;
        await Repository.DeleteByIdsAsync(nullList, false);
        Repository.Save();
        var leftData = await Read.QueryAsync(item => item.IsDeleted != true);
        Assert.True(leftData.Count() == 8);
        leftData = await Read.QueryAsync(item => true);
        Assert.True(leftData.Count() == 20);
    }
    [Fact, Order(24)]
    public async Task ModifyRepositoryDeleteTest()
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
    public async Task ModifyRepositoryDeleteAllTest()
    {
        var delCount = await Repository.DeleteAsync(item => true, true);
        await Repository.SaveAsync();
        var leftData = await Read.QueryAsync(item => true);
        Assert.True(delCount == 8);
        Assert.True(leftData.IsEmpty());
    }

    [Fact, Order(26)]
    public async Task ModifyRepositoryAutoSaveRollBackTestAsync()
    {
        var repo = GetService<IModifyRepository<TestModifyEntity>>();
        await repo.AddAsync(new TestModifyEntity());
        repo.Dispose();
        repo = GetService<IModifyRepository<TestModifyEntity>>();
        var query = await repo.Query().ToListAsync();
        Assert.Empty(query);
    }
    [Fact, Order(26)]
    public async Task ModifyRepositoryAutoSaveTestAsync()
    {
        TransManager.UseAutoCommit();
        IModifyRepository<TestModifyEntity> repo = new ModifyRepository<TestModifyEntity>(GetService<TestDbContext>());
        await repo.AddAsync(new TestModifyEntity());
        repo.Dispose();
        repo = new ModifyRepository<TestModifyEntity>(GetService<TestDbContext>());
        var query = await repo.Query().ToListAsync();
        await repo.DeleteAsync(item => true, true);
        repo.Dispose();
        Assert.NotEmpty(query);
        TransManager.UseAutoCommit(false);
    }
}
