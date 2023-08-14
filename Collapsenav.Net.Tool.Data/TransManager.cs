using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Collapsenav.Net.Tool.Data;

/// <summary>
/// 暂时的做法，后期可能会考虑全部换掉重做
/// </summary>
public class TransManager
{
    public static Dictionary<DbContext, long> ContextCount { get; private set; } = new();

    public static Dictionary<DbContext, IDbContextTransaction> Trans { get; private set; } = new();

    public static bool AutoCommit { get; private set; } = false;
    public static bool HasError { get; private set; } = false;
    public static void UseAutoCommit(bool flag = true)
    {
        AutoCommit = flag;
    }
    public static void HasException(bool flag = true)
    {
        HasError = flag;
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
        if (HasError)
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
        if (Trans.ContainsKey(context))
        {
            var trans = Trans.Pop(context);
            if (trans == null)
                return;
            if (AutoCommit)
                trans.Commit();
            else
                trans.Rollback();
            trans.Dispose();
        }
    }


    public static void CreateTranscation(DbContext context)
    {
        if (Trans.ContainsKey(context))
            return;
        Trans.Add(context, context.Database.BeginTransaction());
    }

    public static void CommitTranscation(DbContext context)
    {
        var trans = Trans.Pop(context);
        if (trans == null)
            return;
        trans.Commit();
    }
}