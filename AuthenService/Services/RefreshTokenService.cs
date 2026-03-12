using System.Security.Cryptography;

public class RefreshTokenService
{
    public string GenerateToken()
    {
        var bytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}