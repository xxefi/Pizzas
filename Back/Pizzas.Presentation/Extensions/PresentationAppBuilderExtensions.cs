using Pizzas.Presentation.Middlewares;

namespace Pizzas.Presentation.Extensions;

public static class PresentationAppBuilderExtensions
{
    public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
    {
        app.UseRequestLocalization();
        app.UseCors();
        app.UseMiddleware<TokenRefreshMiddleware>();
        app.UseMiddleware<UnifiedResponseMiddleware>();
        app.UseMiddleware<SecurityHeadersMiddleware>();
        app.UseMiddleware<CsrfMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}