using FluentValidation.AspNetCore;
using ProductService.BussinessLayer;
using ProductService.DataAccessLayer;
using ProductsService.API.APIEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddBussinessLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
var app = builder.Build();

app.MapProductAPIEndpoints();

app.Run();