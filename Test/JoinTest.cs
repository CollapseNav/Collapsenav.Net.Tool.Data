using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;

[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
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
    [Fact, Order(1)]
    public async Task LeftJoinTest()
    {
        var data = await Repository.CreateJoin()
        .LeftJoin<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .Query
        .Select(item => new
        {
            item.Data1.Number,
            item.Data1.IsTest,
        }).ToListAsync();

        Assert.True(data.Count == 12);
    }

    [Fact, Order(1)]
    public async Task InnerJoinTest()
    {
        var data = await Repository.StartJoin()
        .Join<TestNotBaseModifyEntity>(i => i.Number, i => i.Number)
        .Query
        .Select(item => new
        {
            item.Data1.Number,
            item.Data1.IsTest,
        }).ToListAsync();

        Assert.True(data.Count == 12);
    }
}