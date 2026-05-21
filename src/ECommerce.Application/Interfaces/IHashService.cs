namespace ECommerce.Application.Interfaces;

public interface IHashService
{
    string ComputeHash(string plainText);
    bool CheckHash(string plainText, string hash);
}
