using eCommerce.API.MiddleWare;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using UsersService.Core;
using UsersService.Core.Mappers;
using UsersService.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCore();
builder.Services.AddInfraStructure();
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
builder.Services.AddOpenApi();

var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference();
app.UseExceptionHandlingMiddleware();
app.MapControllers();

app.Run();
