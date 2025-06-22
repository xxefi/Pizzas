using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pizzas.Application.Services.Auth;
using Pizzas.Application.Services.Main;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Auth;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;
using Pizzas.Infrastructure.Repositories.Auth;
using Pizzas.Infrastructure.Repositories.Main;
using Pizzas.Infrastructure.Seeding;
using Pizzas.Infrastructure.UOW;
using Pizzas.Presentation.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseSentry(o =>
{
    var sentryCfg = builder.Configuration.GetSection("Sentry").Get<SentryOptions>();
    o.Dsn = sentryCfg.Dsn;
    o.Debug = sentryCfg.Debug;
    o.TracesSampleRate = sentryCfg.TracesSampleRate;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.HttpContext.GetAccessToken();
            if (!string.IsNullOrEmpty(token))
                context.Token = token;

            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


builder.Services.AddPresentationServices();

builder.Services.Configure<SmtpSettingsEntity>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<PizzasSeeder>();

builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

builder.Services.AddScoped<IUserActiveSessionsRepository, UserActiveSessionsRepository>();
builder.Services.AddScoped<IBlackListedRepository, BlackListedRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IBasketItemRepository, BasketItemRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IDeliveryInfoRepository, DeliveryInfoRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPizzaPriceRepository, PizzaPriceRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlackListedService, BlackListedService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserActiveSessionsService, UserActiveSessionsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IDeliveryInfoService, DeliveryInfoService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IPizzaPriceService, PizzaPriceService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();


builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();


builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
    options.InstanceName = "PizzasApp_"; 
});

var redisConnectionString = builder.Configuration["Redis:Configuration"];


builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnectionString!));

builder.Services.AddSingleton<ISubscriber>(sp =>
    sp.GetRequiredService<IConnectionMultiplexer>().GetSubscriber());

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("https://192.168.0.101:3000", "https://localhost:3000", "https://localhost:3001")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<PizzasContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Pizzas")));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<PizzasSeeder>();
await seeder.SeedAsync();

app.UsePresentation();

app.UseSentryTracing();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
