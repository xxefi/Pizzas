using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Enums;
using static Pizzas.Core.Constants.ValidationConstants;
using static System.Text.RegularExpressions.Regex;

namespace Pizzas.Common.Utilities;

public static class ValidationUtilities
{
    private static readonly List<string> ValidCurrencies = new()
    {"USD", "AZN", "TRY", "RUB"};
    public static bool IsValidPizzaId(string pizzaId) => !string.IsNullOrEmpty(pizzaId);
    public static bool IsValidQuantity(int quantity) => quantity > 0 && quantity <= MaxQuantity;
    public static bool IsValidAddress(string address) => !string.IsNullOrWhiteSpace(address) && address.Length >= 5;
    public static bool IsValidCity(string city) => IsMatch(city, CityRegex);
    public static bool IsValidPostalCode(string postalCode) => IsMatch(postalCode, PostalCodeRegex);
    public static bool IsValidPhoneNumber(string phoneNumber) => IsMatch(phoneNumber, PhoneNumberRegex);
    public static bool IsValidName(string name) => !string.IsNullOrEmpty(name) 
         && name.Length <= MaxProductNameLength && IsMatch(name, ProductNameRegex);
    public static bool IsValidCurrency(string currency) => ValidCurrencies.Contains(currency);
    public static bool IsValidPermissionName(string name) => 
        !string.IsNullOrEmpty(name) && name.Length <= 100;
    public static bool IsValidDescription(string? description) => 
        description == null || description.Length <= 500;
    public static bool IsValidPizzaName(string name) =>
        !string.IsNullOrEmpty(name) && name.Length <= 100;

    public static bool IsValidPizzaDescription(string description) =>
        !string.IsNullOrEmpty(description) && description.Length <= 500;

    public static bool IsValidPrice(decimal? price) => price >= 0;

    public static bool IsValidCreateIngredients(ICollection<CreateIngredientDto> ingredients)
        => ingredients != null && ingredients.Count > 0 && ingredients.All(i => !string.IsNullOrEmpty(i.Name));

    public static bool IsValidUpdateIngredients(ICollection<UpdateIngredientDto> ingredients)
        => ingredients == null || ingredients.All(i => !string.IsNullOrEmpty(i.Name));
    public static bool IsValidPizzaSize(PizzaSize? size) =>
        size == PizzaSize.Small || size == PizzaSize.Medium || size == PizzaSize.Large;
    
    public static bool IsValidCategoryName(string categoryName) => IsMatch(categoryName, CategoryNameRegex);
    public static bool IsValidStreet(string street) => IsMatch(street, StreetRegex);
    public static bool IsValidState(string state) => IsMatch(state, StateRegex);
    public static bool IsValidCountry(string country) => IsMatch(country, CountryRegex);
    
}