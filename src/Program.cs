using erp;
using erp.Controllers;
using erp.Repositories;
using erp.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddSingleton<IAuthenticationService<KeyValuePair<string, string>>, SimpleAuthenticationService>();
builder.Services.AddSingleton<SeriesController>();
builder.Services.AddSingleton<DataAccessLayer>();
builder.Services.AddSingleton<SeriesRepository>();
builder.Services.AddSingleton<StockRepository>();

WebApplication app = builder.Build();
app.MapControllers();
app.UseRouting();

await app.RunAsync();
