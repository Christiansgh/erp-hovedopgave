using erp.Controllers;
using erp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton<IAuthenticationService, SimpleAuthenticationService>();
builder.Services.AddSingleton<ShoeController>();

WebApplication app = builder.Build();
app.MapControllers();
app.UseRouting();

await app.RunAsync();
