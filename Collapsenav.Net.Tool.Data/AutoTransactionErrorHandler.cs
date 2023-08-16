using Microsoft.AspNetCore.Http;

namespace Collapsenav.Net.Tool.Data;

public class AutoTransactionErrorHandler
{
    public static Task Run(HttpContext context)
    {
        TransManager.HasException();
        return Task.CompletedTask;
    }
}