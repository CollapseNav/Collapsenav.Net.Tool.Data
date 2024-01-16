using System.ComponentModel.DataAnnotations;

namespace Collapsenav.Net.Tool.Data.Test;
public class TestEntity : BaseEntity<int>
{
    public TestEntity() { }
    public TestEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}

public class TestReturn
{
    public string Code { get; set; }
    public int? Number { get; set; }
}

public class TestQueryEntity : BaseEntity<int>
{
    public TestQueryEntity() { }
    public TestQueryEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}

public class TestModifyEntity : BaseEntity<int>
{
    public TestModifyEntity() { }
    public TestModifyEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override void SoftDelete()
    {
        base.SoftDelete();
    }
}

public class TestNotBaseEntity : Entity
{
    public TestNotBaseEntity() { }
    public TestNotBaseEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    [Key]
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}

public class TestNotBaseQueryEntity : Entity
{
    public TestNotBaseQueryEntity() { }
    public TestNotBaseQueryEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    [Key]
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}

public class TestNotBaseModifyEntity : Entity
{
    public TestNotBaseModifyEntity() { }
    public TestNotBaseModifyEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    [Key]
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}
public class TestModifyKeyEntity : Entity<int?>
{
    public TestModifyKeyEntity() { }
    public TestModifyKeyEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
}

public class TestModifyAutoKeyEntity : AutoIncrementEntity<int?>
{
    public TestModifyAutoKeyEntity() { }
    public TestModifyAutoKeyEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override void SoftDelete()
    {
        Id = null;
        base.SoftDelete();
    }
}

public class TestModifyAutoEntity : AutoIncrementBaseEntity<int?>
{
    public TestModifyAutoEntity() { }
    public TestModifyAutoEntity(int id, string code, int? number, bool? isTest)
    {
        Id = id;
        Code = code;
        Number = number;
        IsTest = isTest;
    }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override void SoftDelete()
    {
        Id = null;
        base.SoftDelete();
    }
}
