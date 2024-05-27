using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Janet.Core.Services.Security
{
    public class EncryptionEngine
    {
        private string EncryptionKey { get; set; }
        private const int KeySize = 32; // 256-bit key

        public EncryptionEngine()
        {
            EncryptionKey = KeyStorage.RetrieveKey(Constants.EncryptionKeyName);
        }

        public void InitiaizeEncryptionEngine()
        {
            if(!KeyStorage.KeyExists(Constants.EncryptionKeyName)) 
            {
                EncryptionKey = CreateEncryptionKey();
                KeyStorage.SaveKey(Constants.EncryptionKeyName, EncryptionKey);
            }
        }

        public string CreateEncryptionKey()
        {
            byte[] key = new byte[KeySize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return Convert.ToBase64String(key);
        }

        public string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (Aes aesEncryptionAlg = Aes.Create())
            {
                aesEncryptionAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);

                ICryptoTransform encryptor = aesEncryptionAlg.CreateEncryptor(aesEncryptionAlg.Key, aesEncryptionAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string decryptedString = null;

            using (Aes aesDecryptionAlg = Aes.Create())
            {
                aesDecryptionAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);

                ICryptoTransform decryptor = aesDecryptionAlg.CreateDecryptor(aesDecryptionAlg.Key, aesDecryptionAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedString = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedString;
        }
    }
}