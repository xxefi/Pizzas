namespace Pizzas.Core.Dtos.Create;

public class CreateUserActiveSessionDto
{
    public string UserId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string DeviceInfo { get; set; } = string.Empty;
}