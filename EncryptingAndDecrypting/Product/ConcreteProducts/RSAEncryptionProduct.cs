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

        private static RSACryptoServiceProvider _cryptoServiceProvider;
        private static RSAParameters _publicKey;
        private static RSAParameters _privateKey;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public RSAEncryptionProduct()
        {
            //_encryptionType = EncryptionType.RSA;
            _cryptoServiceProvider = new RSACryptoServiceProvider(2048);
            _publicKey = _cryptoServiceProvider.ExportParameters(false);
            _privateKey = _cryptoServiceProvider.ExportParameters(true);
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
            var data = Convert.FromBase64String(text);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.ImportParameters(_publicKey);
                    var encryptedData = rsa.Encrypt(data, false);
                    return Convert.ToBase64String(encryptedData);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public override string Decrypt(string text, string key)
        {
            var data = Convert.FromBase64String(text);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // server decrypting data with private key                    
                    rsa.ImportParameters(_privateKey);
                    var decryptedBytes = rsa.Decrypt(data, false);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                   rsa.PersistKeyInCsp = false;
                }
            }
        }
        #endregion "PublicMethods"

        #region "Helper"
        private string GetKeyString(RSAParameters rsaParameters)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, rsaParameters);
            return stringWriter.ToString();
        }

        #endregion "Helper"
    }
}
