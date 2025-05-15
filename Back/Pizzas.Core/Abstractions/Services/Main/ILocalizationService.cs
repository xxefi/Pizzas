namespace Pizzas.Core.Abstractions.Services.Main;

public interface ILocalizationService
{
    string GetLocalizedString(string key, string? culture = null);
}