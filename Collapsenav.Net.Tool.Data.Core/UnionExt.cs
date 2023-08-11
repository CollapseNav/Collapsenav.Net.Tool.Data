namespace Collapsenav.Net.Tool.Data;

public static class UnionExt
{
    public static IQueryable<T> Union<T>(this IQueryable<T> left, IQueryable<T> right)
    {
        return left.Concat(right);
    }
}