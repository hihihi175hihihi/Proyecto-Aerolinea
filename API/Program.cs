
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Registrar el interceptor personalizado, el middleware y el IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddDbContext<Aerolinea_DesarrolloContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Aerolinea"), option => option.EnableRetryOnFailure());
    
});
//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Agrega el filtro de acción personalizado
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});
// Configure Stripe API key
StripeConfiguration.ApiKey = builder.Configuration["Stripe:ApiKey"];
//Add SMTP como singleton para envios de email
builder.Services.AddSingleton<EmailService>();
//Filtro de Manejo de Excepciones
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionLoggingFilter));
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
