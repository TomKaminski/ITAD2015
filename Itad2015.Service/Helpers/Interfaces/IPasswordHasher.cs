namespace Itad2015.Service.Helpers.Interfaces
{
    public interface IPasswordHasher
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string correctHash, string correctSalt);
        string GetSalt(string hashedPassword);
        string GetHash(string hashedPassword);
    }
}
