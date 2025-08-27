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

        app.MapGet("/", (HttpRequest request, AuthorizationService authorizationService) =>
        {
            if (!AuthorizationService.ValidateAuthorizationCode(request.QueryString.Value!))
            {
                return Results.Redirect(UrlHome + QueryError);
            }

            if (!authorizationService.TryGetAccessToken(out AccessTokenDTO? accessToken))
            {
                return Results.Redirect(UrlHome + QueryError);
            }

            return Results.Redirect(UrlHome);
        });

        app.MapGet("authorize", (AuthorizationService authorizationService) =>
        {
            return authorizationService.GetAuthorizationCodeURL();
        });

        app.MapGet("collect", (AuthorizationService authorizationService) =>
        {
            return Results.Ok(AuthorizationService.CollectVariables());
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.Run();
    }
}