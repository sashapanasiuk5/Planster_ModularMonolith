using System.Security.Cryptography;
using Application.Interfaces;

namespace Application.Services;

public class PasswordHasher: IPasswordHasher
{
    const int Iterations = 10000;
    private const int SaltSize = 16;
    private const int HashSize = 20;
    public string Hash(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
        var hash = pbkdf2.GetBytes(HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
        
        var base64Hash = Convert.ToBase64String(hashBytes);
        return base64Hash;
    }
    
    public bool Verify(string password, string hashedPassword)
    {
        
        /*var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
        var iterations = int.Parse(splittedHashString[0]);
        var base64Hash = splittedHashString[1];*/
        
        var hashBytes = Convert.FromBase64String(hashedPassword);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = pbkdf2.GetBytes(HashSize);
        
        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}