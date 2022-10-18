using Microsoft.AspNetCore.Builder;
using NLayerApp.API.Middlewares;

namespace NLayerApp.API.Extensions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseIpSafe(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpSafe>();
        }

        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

    }
}
