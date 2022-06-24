using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// OUR CODE
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase("BookStoreDb"));
builder.Services.AddScoped<IBookStoreDbContext>(provider=>provider.GetService<BookStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService,DbLogger>();

var app = builder.Build();

// OUR CODE
using (var scope=app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

#region Middleware
/*
app.Run()
app.Run(async context => Console.WriteLine("Middleware 1."));
app.Run(async context => Console.WriteLine("Middleware 2."));

app.Use()
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 1 basladi");
    await next.Invoke();
    Console.WriteLine("Middleware 1 sonlandiriliyor");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 2 basladi");
    await next.Invoke();
    Console.WriteLine("Middleware 2 sonlandiriliyor");
});

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Middleware 3 basladi");
//     await next.Invoke();
//     Console.WriteLine("Middleware 3 sonlandiriliyor");
// });

app.UseHello();

app.Use(async (context, next) =>
{
    Console.WriteLine("Use Middleware tetiklendi");
    await next.Invoke();
});

// app.Map()
app.Map("/example", internalApp => internalApp.Run(async context =>
{
    Console.WriteLine("/example middleware tetiklendi");
    await context.Response.WriteAsync("/example middleware tetiklendi");
}));

// app.MapWhen()
app.MapWhen(x => x.Request.Method == "GET", internalApp =>
{
    internalApp.Run(async context =>
    {
        System.Console.WriteLine("MapWhen Middleware tetiklendi.");
        await context.Response.WriteAsync("MapWhen Middleware tetiklendi.");
    });
});
*/
#endregion

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();
