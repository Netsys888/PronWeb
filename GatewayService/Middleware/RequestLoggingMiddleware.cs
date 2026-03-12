using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        var method = context.Request.Method;
        var path = context.Request.Path;
        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        string body = "";

        if (context.Request.ContentLength > 0)
        {
            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                leaveOpen: true);

            body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;
        }

        Console.WriteLine("===== GATEWAY REQUEST =====");
        Console.WriteLine($"Time: {DateTime.Now}");
        Console.WriteLine($"Method: {method}");
        Console.WriteLine($"Path: {path}");
        Console.WriteLine($"Token: {token}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("===========================");

        await _next(context);   // ⭐ เรียกครั้งเดียว

        Console.WriteLine($"Response Status: {context.Response.StatusCode}");
    }
}