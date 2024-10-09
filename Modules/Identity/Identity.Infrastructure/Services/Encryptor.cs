using System.Security.Cryptography;
using Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class EncryptionOptions
{
    public string Key { get; set; }
    public string IV { get; set; }
    public string Prefix { get; set; }
}

public class Encryptor: IEncryptor
{
    private string _key;
    private string _iv;
    private string _prefix;

    public Encryptor(IOptionsMonitor<EncryptionOptions> options)
    {
        _key = options.CurrentValue.Key;
        _iv = options.CurrentValue.IV;
        _prefix = options.CurrentValue.Prefix;
    }
    public string Encrypt(string text)
    {
        byte[] plaintextBytes = System.Text.Encoding.UTF8.GetBytes(_prefix+text);
        
        
        Aes aes = Aes.Create();
        aes.Key = System.Text.Encoding.UTF8.GetBytes(_key);
        aes.IV = System.Text.Encoding.UTF8.GetBytes(_iv);
        using var ms = new MemoryStream();
        using( var cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write)){
            cs.Write(plaintextBytes, 0, plaintextBytes.Length);
        }
        return Convert.ToBase64String(ms.ToArray());
    }
    
    public string Decrypt(string encrypted)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encrypted);
        
        Aes aes = Aes.Create();
        aes.Key = System.Text.Encoding.UTF8.GetBytes(_key);
        aes.IV = System.Text.Encoding.UTF8.GetBytes(_iv);
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        string result;
        using var ms = new MemoryStream(encryptedBytes);
        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        {
            using (StreamReader srDecrypt = new(cs))
            {
                result = srDecrypt.ReadToEnd();
            }
        }
        return result.Replace(_prefix, "");
    }
}