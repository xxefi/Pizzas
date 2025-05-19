namespace Pizzas.Core.Enums;

public enum PaymentStatus
{
    Pending = 1,
    Processing = 2,
    Authorized = 3,
    Completed = 4,
    Failed = 5, 
    Cancelled = 6,
    Refunded = 7,
    PartiallyRefunded = 8,
    Disputed = 9,
    Expired = 10
}