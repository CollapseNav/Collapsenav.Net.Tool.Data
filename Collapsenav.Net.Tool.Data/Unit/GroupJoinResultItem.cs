namespace Collapsenav.Net.Tool.Data;

public class GroupJoinResultItem<T1>
{
    public T1? Data1 { get; set; }
}

public class GroupJoinResultItem<T1, T2> : GroupJoinResultItem<T1>
{
    public T2? Data2 { get; set; }
}

public class GroupJoinResultItem<T1, T2, T3> : GroupJoinResultItem<T1, T2>
{
    public T3? Data3 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4> : GroupJoinResultItem<T1, T2, T3>
{
    public T4? Data4 { get; set; }
}