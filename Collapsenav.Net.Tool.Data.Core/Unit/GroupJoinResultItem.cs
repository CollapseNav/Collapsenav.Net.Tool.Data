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
public class GroupJoinResultItem<T1, T2, T3, T4, T5> : GroupJoinResultItem<T1, T2, T3, T4>
{
    public T5? Data5 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6> : GroupJoinResultItem<T1, T2, T3, T4, T5>
{
    public T6? Data6 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6>
{
    public T7? Data7 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7>
{
    public T8? Data8 { get; set; }
}
public class GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8, T9> : GroupJoinResultItem<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public T9? Data9 { get; set; }
}