using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.Api
{
    public class RequestLogsMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task InvokeAsync(HttpContext context)
        //{

        //    var request = context.Request;

        //    var requestInfo = $"Time: {DateTime.Now} " +
        //                      $"Protocol: {request.Protocol} " +
        //                      $"HttpMethod: {request.Method} " +
        //                      $"Path: {request.Path} " +
        //                      $"Query: {request.QueryString}" +
        //                      $"Body: {request.Body}";
        //    try
        //    {
        //        File.AppendAllText("AppLogs.log", requestInfo + Environment.NewLine);
        //    }
        //    catch (Exception ex)
        //    {
        //        File.AppendAllText("AppErrorLogs.log", ex.Message.ToString() + Environment.NewLine);
        //    }

        //    await _next(context);
        //}

        public async Task InvokeAsync(HttpContext context)
        {
            var requestContent = string.Empty;

            var request = context.Request;

            if (request.Body.CanRead)
            {
                request.EnableBuffering();
                                
                request.Body.Position = 0;

                requestContent = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync().ConfigureAwait(false);
                
                File.AppendAllText("AppLogs.log", requestContent + Environment.NewLine);

                request.Body.Position = 0;
            }
            
            await _next(context);
        }
    }
}
