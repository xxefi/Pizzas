namespace Pizzas.Core.Dtos.Read;

public class PendingUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
}