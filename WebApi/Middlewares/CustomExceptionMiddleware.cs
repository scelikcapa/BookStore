using System.Diagnostics;

namespace WebApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        string message = "[Request] HTTP "+ context.Request.Method + " - " + context.Request.Path;
        Console.WriteLine(message);

        await next(context);
        watch.Stop();

        message = "[Response] HTTP "+ context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        Console.WriteLine(message);
    }

}

public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}