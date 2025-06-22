using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Pizzas.Application.Mappings;
using Pizzas.Application.Validators.Create;
using Pizzas.RequestPipeline.Commands.Basket;

namespace Pizzas.Presentation.Extensions;

public static class PresentationServiceExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(BasketProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddItemCommand>());

        services.AddAntiforgery(options =>
        {
            options.Cookie.Name = "csrf";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
        });

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[] { new CultureInfo("az"), new CultureInfo("ru"), new CultureInfo("en") };
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }
}