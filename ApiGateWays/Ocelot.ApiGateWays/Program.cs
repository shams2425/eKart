using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("Ocelot.dev.json",optional:false,reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
}
var app = builder.Build();
app.UseOcelot();

app.Run();
