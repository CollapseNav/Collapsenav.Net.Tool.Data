namespace Collapsenav.Net.Tool.Data;

public class GroupJoinResultItem<T1>
{
    public T1? Data1 { get; set; }
}
public class GroupJoinResultItem<T1, T2> : GroupJoinResultItem<T1>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1> data) => Data1 = data.Data1;
    public T2? Data2 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3> : GroupJoinResultItem<T1, T2>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2> data) : base(data) => Data2 = data.Data2;
    public T3? Data3 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4> : GroupJoinResultItem<T1, T2, T3>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3> data) : base(data) => Data3 = data.Data3;

    public T4? Data4 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5> : GroupJoinResultItem<T1, T2, T3, T4>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3, T4> data) : base(data) => Data4 = data.Data4;
    public T5? Data5 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6> : GroupJoinResultItem<T1, T2, T3, T4, T5>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3, T4, T5> data) : base(data) => Data5 = data.Data5;
    public T6? Data6 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3, T4, T5, T6> data) : base(data) => Data6 = data.Data6;
    public T7? Data7 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7> data) : base(data) => Data7 = data.Data7;
    public T8? Data8 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public GroupJoinResultItem() { }
    public GroupJoinResultItem(GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8> data) : base(data) => Data8 = data.Data8;
    public T9? Data9 { get; set; }
}