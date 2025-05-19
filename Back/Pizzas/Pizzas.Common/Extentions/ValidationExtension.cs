using FluentValidation;
using Pizzas.Common.Exceptions;

namespace Pizzas.Common.Extentions;

public static class ValidationExtension
{
    public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, T instance)
    {
        var validationResult = await validator.ValidateAsync(instance);
    
        if (!validationResult.IsValid)
            throw new PizzasException(ExceptionType.Validation, validationResult.Errors.Select(e => e.ErrorMessage)
                .FirstOrDefault());
    }
}