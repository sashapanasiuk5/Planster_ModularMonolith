using System.Security.Cryptography;
using Teams.Domain.Interfaces;

namespace Teams.Domain.Services;

public class RandomStringGenerator: IRandomStringGenerator
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public string Generate(int length)
    {
        var stringChars = new char[length];
        
        for (int i = 0; i < stringChars.Length; i++)
        {
            var index = RandomNumberGenerator.GetInt32(chars.Length);
            stringChars[i] = chars[index];
        }

        return new String(stringChars);
    }
}