using EncryptingAndDecrypting.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptingAndDecrypting.Product.ConcreteProducts
{
    class TripleDESEncryptionProduct : EncryptionProduct
    {
        #region "PrivateFields"
        private readonly EncryptionType _encryptionType;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public TripleDESEncryptionProduct()
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
        public override string Encrypt(string text, string key)
        {
            try
            {
                byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(text);

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider()
                {
                    Key = MysecurityKeyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();
                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);
                MyTripleDESCryptoService.Clear();

                return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override string Decrypt(string text, string key)
        {
            try
            {
                byte[] MyDecryptArray = Convert.FromBase64String(text);

                MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
                byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                MyMD5CryptoService.Clear();

                var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider()
                {
                    Key = MysecurityKeyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();
                byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyDecryptArray, 0, MyDecryptArray.Length);
                MyTripleDESCryptoService.Clear();

                return UTF8Encoding.UTF8.GetString(MyresultArray);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion "PublicMethods"

    }
}
