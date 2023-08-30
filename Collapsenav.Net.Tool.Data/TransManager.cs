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
    /// <summary>
    /// 是否自动提交
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// 默认状态下不开始自动提交, 需要手动调用 save 方法进行更新
    /// </remarks>
    public static bool AutoCommit { get; private set; } = false;
    /// <summary>
    /// 是否报错
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// 当开启自动提交时, 报错后不执行 save 操作
    /// </remarks>
    public static bool HasError { get; private set; } = false;
    public static void UseAutoCommit(bool flag = true)
    {
        AutoCommit = flag;
    }
    public static void HasException(bool flag = true)
    {
        HasError = flag;
    }
    /// <summary>
    /// 注册上下文
    /// </summary>
    /// <param name="context"></param>
    public static void Add(DbContext context)
    {
        // 如果没有开启自动提交, 则该操作失效
        if (!AutoCommit)
            return;
        HasError = false;
        // 进行上下文计数
        if (ContextCount.ContainsKey(context))
            ContextCount[context]++;
        else
            ContextCount.Add(context, 1);
    }
    /// <summary>
    /// 尝试移除上下文
    /// </summary>
    /// <param name="context"></param>
    /// <remarks>
    /// 当repository销毁时进行移除<br/>
    /// 所有repository销毁时才提交事务
    /// </remarks>
    public static void Remove(DbContext context)
    {
        // 如果报错则不执行提交事务的操作
        if (HasError)
        {
            if (ContextCount.ContainsKey(context))
                ContextCount.Pop(context);
            if (Trans.ContainsKey(context))
                Trans.Pop(context);
            return;
        }
        if (ContextCount.ContainsKey(context))
        {
            ContextCount[context]--;
            // 如果context被清除,则尝试提交事务
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