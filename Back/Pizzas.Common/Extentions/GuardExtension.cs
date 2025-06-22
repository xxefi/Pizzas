using Pizzas.Common.Exceptions;

namespace Pizzas.Common.Extentions;

public static class GuardExtension
{
    public static T EnsureFound<T>(this T? entity, string errorMessage)
        where T : class
    => entity ?? throw new PizzasException(ExceptionType.NotFound, errorMessage);
    
}