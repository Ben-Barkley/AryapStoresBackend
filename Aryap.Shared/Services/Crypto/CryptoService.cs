using System.Security.Cryptography;
using System.Text;

namespace Aryap.Shared.Services.Crypto
{
    public class CryptoService : ICryptoService
    {
        // Encrypts plain text using AES encryption
        public string Encrypt(string key, string plainText)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Encryption key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentException("Plain text cannot be null or empty.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                // Derive a 256-bit key from the provided key string
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] derivedKey = new byte[32]; // AES-256 requires a 32-byte key
                Array.Copy(keyBytes, derivedKey, Math.Min(keyBytes.Length, derivedKey.Length));
                aesAlg.Key = derivedKey;

                // Generate a random IV (Initialization Vector)
                aesAlg.GenerateIV();
                byte[] iv = aesAlg.IV;

                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Encrypt the plain text
                byte[] encryptedBytes;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length); // Write the IV to the beginning of the stream
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }

                // Return the encrypted data as a Base64-encoded string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Decrypts cipher text using AES decryption
        public string Decrypt(string key, string cipherText)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Decryption key cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentException("Cipher text cannot be null or empty.");
            }

            using (Aes aesAlg = Aes.Create())
            {
                // Derive a 256-bit key from the provided key string
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] derivedKey = new byte[32]; // AES-256 requires a 32-byte key
                Array.Copy(keyBytes, derivedKey, Math.Min(keyBytes.Length, derivedKey.Length));
                aesAlg.Key = derivedKey;

                // Extract the IV from the beginning of the cipher text
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] iv = new byte[16]; // AES IV is always 16 bytes
                Array.Copy(cipherBytes, iv, iv.Length);
                aesAlg.IV = iv;

                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Decrypt the cipher text
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}