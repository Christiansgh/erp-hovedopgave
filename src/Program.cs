var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

WebApplication app = builder.Build();

Console.ReadLine();
await app.RunAsync();
