namespace Application.Interfaces;

public interface IEncryptor
{
    string Encrypt(string text);
    string Decrypt(string text);
}