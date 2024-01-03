using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
[Collection("a")]
public class JoinTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IModifyRepository<TestModifyEntity> Repository;
    protected readonly IQueryRepository<TestModifyEntity> Read;
    public JoinTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IModifyRepository<TestModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }
    [Fact, Order(0)]
    public async Task LeftJoinTest()
    {
        var data = await Repository.CreateJoin()
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .Select(item => new
        {
            item.Data1.Number,
            item.Data1.IsTest,
        }).ToListAsync();

        Assert.True(data.Count == 12);
    }

    [Fact, Order(0)]
    public async Task InnerJoinTest()
    {
        var data = await Repository.StartJoin()
        .Join<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .Select(item => new
        {
            item.Data1.Number,
            item.Data1.IsTest,
        }).ToListAsync();

        Assert.True(data.Count == 12);
    }

    [Fact, Order(0)]
    public async Task UnionTest()
    {
        var data = await Repository.Query()
        .Where(item => item.Id > 0)
        .Union(Repository.Query().Where(item => item.Id == 0))
        .ToListAsync();

        Assert.True(data.Count == 10);
    }

    [Fact, Order(1)]
    public async Task EightLeftJoinTest()
    {
        var data = await Repository.CreateJoin()
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data2.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data3.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data4.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data5.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data6.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data7.Number, i => i.Number)
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Data8.Number, i => i.Number)
        .Select(item => new
        {
            Number1 = item.Data1.Number,
            Number2 = item.Data2.Number,
            Number3 = item.Data3.Number,
            Number4 = item.Data4.Number,
            Number5 = item.Data5.Number,
            Number6 = item.Data6.Number,
            Number7 = item.Data7.Number,
            Number8 = item.Data8.Number,
            Number9 = item.Data9.Number,
        }).ToListAsync();

        Assert.True(data.Count == 520);

        try
        {
            data = await Repository.CreateJoin()
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data2.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data3.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data4.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data5.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data6.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data7.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data8.Number, i => i.Number)
            .LeftJoin<TestNotBaseModifyEntity>(i => i.Data8.Number, i => i.Number)
            .Select(item => new
            {
                Number1 = item.Data1.Number,
                Number2 = item.Data2.Number,
                Number3 = item.Data3.Number,
                Number4 = item.Data4.Number,
                Number5 = item.Data5.Number,
                Number6 = item.Data6.Number,
                Number7 = item.Data7.Number,
                Number8 = item.Data8.Number,
                Number9 = item.Data9.Number,
            }).ToListAsync();
        }
        catch (Exception ex)
        {
            Assert.Equal("求求了，别再联表了", ex.Message);
        }
    }
    [Fact, Order(2)]
    public async Task EightJoinTest()
    {
        var data = await Repository.CreateJoin()
        .Join<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data2.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data3.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data4.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data5.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data6.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data7.Number, i => i.Number)
        .Join<TestNotBaseModifyEntity>(i => i.Data8.Number, i => i.Number)
        .Select(item => new
        {
            Number1 = item.Data1.Number,
            Number2 = item.Data2.Number,
            Number3 = item.Data3.Number,
            Number4 = item.Data4.Number,
            Number5 = item.Data5.Number,
            Number6 = item.Data6.Number,
            Number7 = item.Data7.Number,
            Number8 = item.Data8.Number,
            Number9 = item.Data9.Number,
        }).ToListAsync();

        Assert.True(data.Count == 520);

        try
        {
            data = await Repository.CreateJoin()
            .Join<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data2.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data3.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data4.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data5.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data6.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data7.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data8.Number, i => i.Number)
            .Join<TestNotBaseModifyEntity>(i => i.Data9.Number, i => i.Number)
            .Select(item => new
            {
                Number1 = item.Data1.Number,
                Number2 = item.Data2.Number,
                Number3 = item.Data3.Number,
                Number4 = item.Data4.Number,
                Number5 = item.Data5.Number,
                Number6 = item.Data6.Number,
                Number7 = item.Data7.Number,
                Number8 = item.Data8.Number,
                Number9 = item.Data9.Number,
            }).ToListAsync();
        }
        catch (Exception ex)
        {
            Assert.Equal("求求了，别再联表了", ex.Message);
        }
    }
}