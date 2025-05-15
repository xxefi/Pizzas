namespace Pizzas.Common.Exceptions;

public class PizzasException : Exception
{
    public ExceptionType ExceptionType { get; set; }
    public PizzasException(ExceptionType exceptionType, string message) : base(message)
        => ExceptionType = exceptionType;
}