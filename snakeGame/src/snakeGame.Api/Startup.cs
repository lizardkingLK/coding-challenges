using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using snakeGame.Api.Abstractions;
using snakeGame.Api.Services;

namespace snakeGame.Api;

public class Startup
{
    public static void Run<T>(string apiUrl)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Services.Configure<ConsoleLifetimeOptions>(options =>
        {
            options.SuppressStatusMessages = true;
        });

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });

        builder.Services.AddSingleton<IListener<T>, ListenerService<T>>();

        builder.WebHost.UseUrls(apiUrl);

        builder.Logging.ClearProviders();

        WebApplication app = builder.Build();

        app.UseCors("AllowAnyOrigin");

        app.UseDefaultFiles(new DefaultFilesOptions
        {
            DefaultFileNames = ["index.html"]
        });

        app.UseStaticFiles();

        app.MapPost(
            "/update",
            (HttpContext httpContext, IListener<T> listenerService, CancellationToken cancellationToken) =>
        {
            T? body = JsonSerializer.Deserialize<T>(httpContext.Request.Body);
            if (body == null)
            {
                return Task.FromResult(HttpStatusCode.BadRequest);
            }

            listenerService.Notify(body);

            return Task.FromResult(HttpStatusCode.OK);
        });

        app.MapGet(
            "/events",
            async (HttpContext httpContext, IListener<T> listenerService, CancellationToken cancellationToken) =>
        {
            httpContext.Response.Headers.ContentType = "text/event-stream";

            while (!cancellationToken.IsCancellationRequested)
            {
                T updatedItem = await listenerService.Wait();

                await httpContext.Response.WriteAsync("data: ", cancellationToken);
                await JsonSerializer.SerializeAsync(httpContext.Response.Body, updatedItem, cancellationToken: cancellationToken);
                await httpContext.Response.WriteAsync("\n\n", cancellationToken);
                await httpContext.Response.Body.FlushAsync(cancellationToken);

                listenerService.Reset();
            }
        });

        app.Run();
    }
}
