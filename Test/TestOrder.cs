using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

// 所有测试集合共享同一个 Test.db，且集合间存在数据依赖（如 "d" 集合会写入 "1" 集合读取的 NoConstraintsTestQuerys），
// 因此禁用并行，并按集合 DisplayName 排序（"1"→"a"→…→"f"）串行执行。
[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer("Collapsenav.Net.Tool.Data.Test.TestCollection", "Collapsenav.Net.Tool.Data.Test")]

namespace Collapsenav.Net.Tool.Data.Test;
[AttributeUsage(AttributeTargets.Method)]
public class OrderAttribute : Attribute
{
    public int Sort { get; set; }
    public OrderAttribute(int sort)
    {
        this.Sort = sort;
    }
}
public class TestOrders : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string typeName = typeof(OrderAttribute).AssemblyQualifiedName; ;
        var result = testCases.ToList();
        result.Sort((x, y) =>
        {
            var xOrder = x.TestMethod.Method.GetCustomAttributes(typeName)?.FirstOrDefault();
            if (xOrder == null)
            {
                return 0;
            }
            var yOrder = y.TestMethod.Method.GetCustomAttributes(typeName)?.FirstOrDefault();
            if (yOrder == null)
            {
                return 0;
            }
            var sortX = xOrder.GetNamedArgument<int>("Sort");
            var sortY = yOrder.GetNamedArgument<int>("Sort");
            return sortX - sortY;
        });
        return result;
    }
}

public class TestCollection : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        var result = testCollections.OrderBy(collection => collection.DisplayName);
        var logPath = "./dads.txt";
        var logText = string.Empty;
        if (File.Exists(logPath))
            logText = File.ReadAllText(logPath);
        logText += result.Select(item => item.DisplayName).Join("\n");
        logText.ToBytes().SaveTo(logPath);
        return result;
    }
}
