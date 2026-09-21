using OrderService.API.MiddleWare;
using OrderService.BussinessLayer;
using OrderService.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBussinessLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:4200")
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandlingMiddleware();

app.UseRouting();

//CORS
app.UseCors();

// SWAGGER
app.UseSwagger();
app.UseSwaggerUI();

//AUTHENTICATION
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
