namespace Pizzas.Core.Dtos.Auth;

public class AccessInfoDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}