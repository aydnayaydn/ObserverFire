using Microsoft.EntityFrameworkCore;
using ObserverFire.Abstractions;
using ObserverFire.Api;
using ObserverFire.Api.Middlewares;
using ObserverFire.Data;
using ObserverFire.Repositories;
using ObserverFire.Service;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MainContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    // Request scope içinde IUserService çözümlemesi
    var userService = context.RequestServices.GetRequiredService<IUserService>();
    var customAuthMiddleware = new CustomAuthorizationMiddleware(next, userService);
    await customAuthMiddleware.InvokeAsync(context);
});
app.ConfigureEndpoints();
app.ConfigureUserEndpoints();

app.Run();