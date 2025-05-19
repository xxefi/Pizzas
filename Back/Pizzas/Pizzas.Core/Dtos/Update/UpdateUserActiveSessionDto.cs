namespace Pizzas.Core.Dtos.Update;

public class UpdateUserActiveSessionDto
{
    public string UserId { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryDate { get; set; }
    public string DeviceInfo { get; set; } = string.Empty;
}