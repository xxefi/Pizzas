using System.Globalization;
using System.Resources;
using Pizzas.Core.Abstractions.Services.Main;

namespace Pizzas.Application.Services.Main;

public class LocalizationService : ILocalizationService
{
    private readonly string _defaultCulture = "en";
    private readonly Dictionary<string, Dictionary<string, ResourceManager>> _resourceManagers = new()
    {
        {
            "Errors",
            new()
            {
                { "en", new ResourceManager("Pizzas.Application.Resources.Errors.ErrorsMessages", typeof(LocalizationService).Assembly) },
                { "ru", new ResourceManager("Pizzas.Application.Resources.Errors.ErrorsMessages", typeof(LocalizationService).Assembly) },
                { "az", new ResourceManager("Pizzas.Application.Resources.Errors.ErrorsMessages", typeof(LocalizationService).Assembly) }
            }
        },  
    };
    
    public string GetLocalizedString(string key, string? culture = null)
    {
        culture ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        if (!_resourceManagers.Values.SelectMany(dict => dict.Keys).Contains(culture))
            culture = _defaultCulture;

        foreach (var resourceDict in _resourceManagers.Values)
        {
            if (resourceDict.TryGetValue(culture, out var resourceManager))
            {
                var result = resourceManager.GetString(key);
                if (!string.IsNullOrEmpty(result)) return result;
            }
        }
    
        return key; 
    }
}