using EncryptingAndDecrypting.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EncryptingAndDecrypting.Product.ConcreteProducts
{
    /// <summary>  
    /// RSA EncryptionProduct 'ConcreteProduct' class  
    /// </summary> 
    class RSAEncryptionProduct : EncryptionProduct
    {
        #region "PrivateFields"
        private readonly EncryptionType _encryptionType;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public RSAEncryptionProduct()
        {
            _encryptionType = EncryptionType.RSA;
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
            var data = Encoding.UTF8.GetBytes(text);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(key);
                    var encryptedData = rsa.Encrypt(data, true);
                    return Convert.ToBase64String(encryptedData);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public override string Decrypt(string text, string key)
        {
            var data = Encoding.UTF8.GetBytes(text);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var base64Encrypted = text;
                    // server decrypting data with private key                    
                    rsa.FromXmlString(key);
                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    return Encoding.UTF8.GetString(decryptedBytes).ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        #endregion "PublicMethods"

    }
}
