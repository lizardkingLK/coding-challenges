internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Host.UseWindowsService();
        builder.Services.AddWindowsService();

        builder.Services.AddOpenApi();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapGet("/", () =>
        {
            return Results.Redirect("/index.html");
        });

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.Run();
    }
}
