using erp;
using erp.Controllers;
using erp.Repositories;
using erp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton<IAuthenticationService, SimpleAuthenticationService>();
builder.Services.AddSingleton<ShoeController>();
builder.Services.AddSingleton<DataAccessLayer>();
builder.Services.AddSingleton<ShoeRepository>();

WebApplication app = builder.Build();
app.MapControllers();
app.UseRouting();

await app.RunAsync();
