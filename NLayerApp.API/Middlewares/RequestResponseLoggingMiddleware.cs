using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Entities;
using NLayerApp.Core.Models;
using NLayerApp.Core.Services;
using NLayerApp.Service.Exceptions;
using NLayerApp.Service.Services;
using System;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace NLayerApp.API.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private const string DEFAULT_ERROR_MESSAGE = "An error occured, please contact with team.";
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly ILogService _logService;
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _logService = logService;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment environment)
        {
            LogCreateModel log = new LogCreateModel();
            log.LogDate = DateTime.Now;
            log = await LogRequest(context, log);

            Claim userClaim = context.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userClaim != null)
            {
                log.UserId = userClaim.Value;
            }

            log = await LogRequest(context, log);
            log = await LogResponse(context, environment, log);
            _ = _logService.CreateLog(log);

        }

        private async Task<LogCreateModel> LogRequest(HttpContext context, LogCreateModel log)
        {
            try
            {
                context.Request.EnableBuffering();
                using var requestStream = _recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream);
                log.RequestBody = ReadStreamInChunks(requestStream);
                log.Path = context.Request.Path;
                log.Host = context.Request.Host.Value;
                log.QueryString = context.Request.QueryString.ToString();
                log.RemoteIp = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                log.Headers = string.Join(",", context.Request.Headers.Select(he => he.Key + ":[" + he.Value + "]").ToList());
                context.Request.Body.Position = 0;
            }
            catch (Exception exception)
            {

                log.Exception = JsonSerializer.Serialize(exception);
                log.ExceptionMessage = JsonSerializer.Serialize(exception.Message);
                if (exception.InnerException != null)
                {
                    log.InnerException = JsonSerializer.Serialize(exception.InnerException);
                    log.InnerExceptionMessage = JsonSerializer.Serialize(exception.InnerException.Message);
                }
            }

            return log;
        }

        private async Task<LogCreateModel> LogResponse(HttpContext context, IWebHostEnvironment environment, LogCreateModel log)
        {

            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                List<string> errors = new List<string>();
                log.Exception = JsonSerializer.Serialize(exception);
                errors.Add(exception.ToString());
                log.ExceptionMessage = JsonSerializer.Serialize(exception.Message);
                errors.Add(GetErrorMessage(exception, environment));
                if (exception.InnerException != null)
                {
                    log.InnerException = JsonSerializer.Serialize(exception.InnerException);
                    errors.Add(exception.InnerException.ToString());
                    log.InnerExceptionMessage = JsonSerializer.Serialize(exception.InnerException.Message);
                    errors.Add(GetErrorMessage(exception.InnerException, environment));
                }

                context.Response.ContentType = "application/json";

                switch (exception)
                {
                    case AppException e:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException n:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var response = CustomResponseDto<NoContentDto>.Fail(context.Response.StatusCode, errors: errors, null, false);
                string jsonString = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonString);
            }
            finally
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                log.ResponseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            return log;
        }

        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        public string GetErrorMessage(Exception exception, IWebHostEnvironment envorinmet)
        {

            if (envorinmet.IsDevelopment())
            {
                return exception.Message;
            }
            else
            {
                return DEFAULT_ERROR_MESSAGE;
            }
        }


    }
}
