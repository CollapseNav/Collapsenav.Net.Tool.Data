namespace Collapsenav.Net.Tool.Data;
public class PageRequest
{
    public virtual int Index { get; set; } = 1;
    public virtual int Max { get; set; } = 20;
    /// <summary>
    /// skip = (index - 1) * max
    /// </summary>
    /// <value></value>
    public virtual int Skip
    {
        get => (Index - 1) * Max;
    }
}
