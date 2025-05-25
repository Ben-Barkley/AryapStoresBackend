namespace Aryap.Shared.Services.Crypto
{
    public interface ICryptoService
    {
        string Encrypt(string key, string plainText);
        string Decrypt(string key, string cipherText);
    }
}