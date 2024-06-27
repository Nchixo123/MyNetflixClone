using Models;

namespace ServiceInterfaces;

public interface ITokenService
{ 
    AuthResponse GenerateToken(string username);
    AuthResponse RefreshToken(string token);
}
