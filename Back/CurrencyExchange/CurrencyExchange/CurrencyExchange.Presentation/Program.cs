using CurrencyExchange.Application.Services;
using CurrencyExchange.Domain.Abstractions.Repositories;
using CurrencyExchange.Domain.Abstractions.Services;
using CurrencyExchange.Infrastructure.Context;
using CurrencyExchange.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("https://192.168.2.163:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

builder.Services.AddDbContext<CurrencyExchangeContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("CurrencyExchange")));

builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();
builder.Services.AddScoped<ICurrencyExchangeRateService, CurrencyExchangeRateService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapControllers();

app.UseCors();


app.Run();

