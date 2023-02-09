using EncryptingAndDecrypting.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptingAndDecrypting.Product.ConcreteProducts
{
    /// <summary>  
    /// AES Encryption 'ConcreteProduct' class  
    /// </summary> 
    class AESEncryptionProduct : EncryptionProduct
    {
        #region "PrivateFields"
        private readonly EncryptionType _encryptionType;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public AESEncryptionProduct()
        {
            _encryptionType = EncryptionType.AES;
        }
        #endregion "PublicConstructor"

        #region "PublicProperties"
        public override EncryptionType EncryptionType     
        {
            get { return _encryptionType; }
        }

        #endregion "PublicProperties"

        #region "PublicMethods"
        public override string Encrypt(string clearText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"Encryption key is empty!");
                }
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public override string Decrypt(string cipherText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception($"Encryption key is empty!");
                }
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion "PublicMethods"

    }
}
