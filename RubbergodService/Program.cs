using Microsoft.AspNetCore.HttpOverrides;
using RubbergodService.Data;
using RubbergodService.Data.Discord;
using RubbergodService.Data.Repository;
using RubbergodService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services
    .AddDatabase(builder.Configuration, out var connectionString)
    .AddManagers()
    .AddDiscord()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDirectApi()
    .AddScoped<RequestCounterMiddleware>();
builder.Services.AddControllers();

builder.Services
    .AddHealthChecks()
    .AddNpgSql(connectionString);
builder.Services.Configure<ForwardedHeadersOptions>(opt => opt.ForwardedHeaders = ForwardedHeaders.All);

var app = builder.Build();

app.Services.GetRequiredService<DiscordLogManager>();
using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<RubbergodServiceRepository>()
    .ProcessMigrations();
await scope.ServiceProvider.GetRequiredService<IDiscordManager>().LoginAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestCounterMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
