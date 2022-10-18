using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Service.Exceptions;
using NLog;
using NLog.Web;
using System.Text.Json;

namespace NLayerApp.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            List<string> errors = new List<string>();

            app.UseExceptionHandler(config =>
            {
                config.Run(
                 async context =>
                 {
                     var nLogger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
                     LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
                     context.Response.ContentType = "application/json";
                     var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                     var statusCode = exceptionFeature.Error switch
                     {
                         ClientSideException => 400,
                         NotFoundException => 404,
                         _ => 500
                     };
                     context.Response.StatusCode = statusCode;
                     nLogger.Error(exceptionFeature.Error, exceptionFeature.Error.Message);
                     errors.Add(exceptionFeature.Error.Message);

                     if (exceptionFeature.Error.InnerException != null)
                     {
                         nLogger.Error(exceptionFeature.Error.InnerException, exceptionFeature.Error.InnerException.Message);
                         errors.Add(exceptionFeature.Error.InnerException.Message);
                     }

                     var response = CustomResponseDto<NoContentDto>.Fail(statusCode, errors: errors, null, false);
                     await context.Response.WriteAsync(JsonSerializer.Serialize(response));


                 });
            });
        }
    }
}
