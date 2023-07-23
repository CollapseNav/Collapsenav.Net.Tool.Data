using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data;

public class TransManager
{
    private static Dictionary<DbContext, long> ContextCount { get; set; } = new();
    public static void Add(DbContext context)
    {
        if (ContextCount.ContainsKey(context))
            ContextCount[context]++;
        else
            ContextCount.Add(context, 1);
    }

    public static void Remove(DbContext context)
    {
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