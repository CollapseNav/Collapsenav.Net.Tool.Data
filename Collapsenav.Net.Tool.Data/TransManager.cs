using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class TransManager
{
    private static Dictionary<DbContext, long> ContextCount { get; set; } = new();
    public static bool AutoCommit = false;
    public static bool HasError = false;
    public static void UseAutoCommit()
    {
        AutoCommit = true;
    }
    public static void HasException()
    {
        HasError = true;
    }
    public static void Add(DbContext context)
    {
        if (!AutoCommit)
            return;
        HasError = false;
        if (ContextCount.ContainsKey(context))
            ContextCount[context]++;
        else
            ContextCount.Add(context, 1);
    }
    public static void Remove(DbContext context)
    {
        if (!HasError)
            return;
        if (ContextCount.ContainsKey(context))
        {
            ContextCount[context]--;
            if (ContextCount[context] == 0)
            {
                ContextCount.Remove(context);
                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();
            }
        }
    }
}