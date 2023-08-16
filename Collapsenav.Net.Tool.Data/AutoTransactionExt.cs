using Microsoft.AspNetCore.Builder;
namespace Collapsenav.Net.Tool.Data;
public static class TransactionActionExt
{
    public static IApplicationBuilder UseAutoCommit(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(AutoTransactionErrorHandler.Run);
        });
        TransManager.UseAutoCommit();
        return app;
    }
}