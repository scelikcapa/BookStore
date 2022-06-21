namespace WebApi.Middlewares;

public class HelloMiddleware
{
    private readonly RequestDelegate next;

    public HelloMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        System.Console.WriteLine("Hello World");
        await next.Invoke(context);
        System.Console.WriteLine("Bye World");
    }
}

static public class HelloMiddlewareExtension
{
    public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloMiddleware>();
    }
}