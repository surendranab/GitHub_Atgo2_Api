using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Atgo2.Api.CrossCuttingLayer.Utilities
{
    public class Encryption
    {
        private const string DefaultEncryptionKey = "E546C8DF278CD5931069B522E695D4F2";
        public static string EncryptString(string text, string keyString = DefaultEncryptionKey)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    aesAlg.Padding = PaddingMode.Zeros;
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        
                        return Base64UrlEncoder.Encode(Convert.ToBase64String(result));
                    }
                }
            }
        }

        public static string DecryptString(string cipherText, string keyString = DefaultEncryptionKey)
        {
            var fullCipher = Convert.FromBase64String(Base64UrlEncoder.Decode(cipherText));

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    aesAlg.Padding = PaddingMode.Zeros;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return  result;
                }
            }
        }

        public static string GenerateSalt()
        {
            using (RandomNumberGenerator randomNumber = RandomNumberGenerator.Create())
            {
                byte[] tokenData = new byte[32];
                randomNumber.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }

    }
}
