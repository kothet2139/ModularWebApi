namespace ModularWebApi.Modules.User.Domain
{
    public interface IJWTProvider
    {
        string GenerateToken(Guid userId, string role);
        bool ValidateToken(string token);
    }
}
