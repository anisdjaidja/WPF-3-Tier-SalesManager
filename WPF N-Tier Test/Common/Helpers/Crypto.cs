using System.IO;
using System.Security.Cryptography;

namespace WPF_N_Tier_Test.Modules.Helpers
{
    public static class Crypto
    {
        private static Rfc2898DeriveBytes DerivePassword()
        {
            // Derive a new password using the PBKDF2 algorithm and a random salt
            return new Rfc2898DeriveBytes("tajir", 20);
        }
        public static string Encrypt(string text)
        {
            // Convert the plaintext string to a byte array
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(text);
            Rfc2898DeriveBytes passwordBytes = DerivePassword();
            
            // Use the password to encrypt the plaintext
            Aes encryptor = Aes.Create();
            encryptor.Key = passwordBytes.GetBytes(32);
            encryptor.IV = passwordBytes.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string Decrypt(string encrypted)
        {
            // Convert the encrypted string to a byte array
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            Rfc2898DeriveBytes passwordBytes = DerivePassword();

            // Use the password to decrypt the encrypted string
            Aes encryptor = Aes.Create();
            encryptor.Key = passwordBytes.GetBytes(32);
            encryptor.IV = passwordBytes.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
