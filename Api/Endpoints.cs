namespace ObserverFire.Api;

public static class Endpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapGet("/connection", () =>
        {
            return "ONLINE";
        })
        .WithName("connection")
        .WithOpenApi();
    }
}