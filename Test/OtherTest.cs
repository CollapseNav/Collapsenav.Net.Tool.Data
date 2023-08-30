using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;

public class OtherTest
{
    [Fact]
    public void NoBaseKeyTypeTest()
    {
        var entity = new TestNotBaseModifyEntity();
        Assert.True(entity.KeyType() == typeof(int?));
    }

    [Fact]
    public void NoBaseKeyPropTest()
    {
        var entity = new TestNotBaseModifyEntity();
        Assert.True(entity.KeyProp().PropertyType == typeof(int?));
    }

    [Fact]
    public void KeyTypeTest()
    {
        var entity = new TestModifyEntity();
        Assert.True(entity.KeyType() == typeof(int));
    }

    [Fact]
    public void KeyPropTest()
    {
        var entity = new TestModifyEntity();
        Assert.True(entity.KeyProp().PropertyType == typeof(int));
    }

    [Fact]
    public void KeyTypeKeyEntityTest()
    {
        var entity = new TestModifyKeyEntity();
        Assert.True(entity.KeyType() == typeof(int?));
    }
    [Fact]
    public void KeyPropKeyEntityTest()
    {
        var entity = new TestModifyKeyEntity();
        Assert.True(entity.KeyProp().PropertyType == typeof(int?));
    }
    [Fact]
    public void KeyTypeAutoKeyEntityTest()
    {
        var entity = new TestModifyAutoKeyEntity();
        Assert.True(entity.KeyType() == typeof(int?));
    }
    [Fact]
    public void KeyPropAutoKeyEntityTest()
    {
        var entity = new TestModifyAutoKeyEntity();
        Assert.True(entity.KeyProp().PropertyType == typeof(int?));
    }
    [Fact]
    public void KeyTypeAutoEntityTest()
    {
        var entity = new TestModifyAutoEntity();
        Assert.True(entity.KeyType() == typeof(int?));
    }
    [Fact]
    public void KeyPropAutoEntityTest()
    {
        var entity = new TestModifyAutoEntity();
        Assert.True(entity.KeyProp().PropertyType == typeof(int?));
    }
}