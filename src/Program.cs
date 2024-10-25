using erp_hovedopgave.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

WebApplication app = builder.Build();

ConnectionPool.PrintFoundString();
Console.ReadLine();
await app.RunAsync();
