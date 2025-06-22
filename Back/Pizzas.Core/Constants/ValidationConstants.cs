namespace Pizzas.Core.Constants;

public static class ValidationConstants
{
    public const int MaxBasketNameLength = 100;
    public const int MaxProductNameLength = 200;
    public const int MaxProductDescriptionLength = 1000;
    public const int MaxQuantity = 10000;
    public const int MaxCategoryNameLength = 50;
    public const int MaxStreetLength = 100;
    public const int MaxCityLength = 50;
    public const int MaxStateLength = 50;
    public const int MaxCountryLength = 50;
    public const int MaxPostalCodeLength = 20;
    
    public const string BasketNameRegex = @"^[a-zA-Z0-9\s\-]+$";
    public const string ProductNameRegex = @"^[a-zA-Z0-9\s\-\.,]+$";
    public const string ProductDescriptionRegex = @"^[a-zA-Z0-9\s\-\.,:;]+$";
    public const string CityRegex = @"^[a-zA-Z\s\-]+$";
    public const string PostalCodeRegex = @"^\d{5}(-\d{4})?$";
    public const string PhoneNumberRegex = @"^\+?[1-9]\d{1,14}$";
    public const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public const string CategoryNameRegex = @"^[a-zA-Z0-9\s-]+$";
    public const string StreetRegex = @"^[a-zA-Z0-9\s\.,#-]+$";
    public const string StateRegex = @"^[a-zA-Z\s-]+$";
    public const string CountryRegex = @"^[a-zA-Z\s-]+$";

}