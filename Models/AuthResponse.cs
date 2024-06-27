namespace Models;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime Expiry { get; set; }

    public AuthResponse(string token, DateTime expiry)
    {
        Token = token;
        Expiry = expiry;
    }
}