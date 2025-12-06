using System.Text.Json;
using lizzy.Core.Configurations;
using lizzy.Core.Services;
using lizzy.Core.State;
using Microsoft.Extensions.Options;
using static lizzy.Core.Shared.Constants;

namespace lizzy.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Host.UseWindowsService();
        builder.Services.AddWindowsService();

        builder.Services.AddSingleton<AuthorizationListenerService>();

        builder.Services.Configure<SpotifyOptions>(
            builder.Configuration.GetSection(nameof(SpotifyOptions)));

        builder.Services.AddHttpClient<AuthorizationService>((serviceProvider, httpClient) =>
        {
            SpotifyOptions spotifyOptions = serviceProvider.GetRequiredService<IOptions<SpotifyOptions>>().Value;
            httpClient.BaseAddress = new Uri(spotifyOptions.BaseAddress);
        });

        builder.Services.AddOpenApi();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapGet("/", (
            HttpRequest request,
            AuthorizationService authorizationService,
            AuthorizationListenerService authorizationListenerService) =>
        {
            if (!AuthorizationService.ValidateAuthorizationCode(request.QueryString.Value!))
            {
                return Results.Redirect(UrlHome + QueryError);
            }

            if (!authorizationService.TryGetAccessToken(out AccessTokenDTO? accessToken))
            {
                return Results.Redirect(UrlHome + QueryError);
            }

            authorizationListenerService.SetAuthorizeCompleted(accessToken!);

            return Results.Redirect(UrlHome);
        });

        app.MapGet("authorize", (AuthorizationService authorizationService) =>
        {
            return authorizationService.GetAuthorizationCodeURL();
        });

        app.MapGet("listen", async (
            HttpContext httpContext,
            AuthorizationListenerService authorizationListenerService,
            CancellationToken cancellationToken) =>
        {
            httpContext.Response.Headers.ContentType = "text/event-stream";

            while (!cancellationToken.IsCancellationRequested)
            {
                AccessTokenDTO accessToken = await authorizationListenerService.WaitForAuthorize();

                await JsonSerializer.SerializeAsync(
                    httpContext.Response.Body,
                    accessToken,
                    cancellationToken: cancellationToken);

                await httpContext.Response.Body.FlushAsync(cancellationToken);

                authorizationListenerService.ResetAuthorizeWait();
            }
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.Run();
    }
}