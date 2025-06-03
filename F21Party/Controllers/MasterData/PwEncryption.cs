using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F21Party.Controllers
{
    internal class PwEncryption
    {
        private static readonly byte[] _iv = new byte[16]; // 16-byte IV (Initialization Vector)
        private static readonly string _myKey = "123"; 
        private static readonly byte[] _aesKey = GetAesKeyFromString(_myKey); // AES key derived from the simple key

        // Convert simple key into a valid AES key (using SHA256)
        private static byte[] GetAesKeyFromString(string key)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(key)); // Always 32 bytes (AES-256)
            }
        }

        // Encrypt a password with the given simple key
        public static string Encrypt(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _aesKey; // Use the derived AES key
                aes.IV = _iv; // IV is fixed (for simplicity, but typically random)

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(password); // Encrypt the password
                        }
                        byte[] encrypted = ms.ToArray();
                        return Convert.ToBase64String(encrypted); // Return base64 string of encrypted password
                    }
                }
            }
        }

        // Decrypt a password with the given simple key
        public static string Decrypt(string encryptedPassword)
        {
            byte[] buffer = Convert.FromBase64String(encryptedPassword);

            using (Aes aes = Aes.Create())
            {
                aes.Key = _aesKey; // Use the derived AES key
                aes.IV = _iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd(); // Decrypt and return the original password
                        }
                    }
                }
            }
        }
    }
}
