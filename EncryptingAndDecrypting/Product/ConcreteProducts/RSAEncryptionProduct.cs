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

        private RSAParameters _publicKey;
        private RSAParameters _privateKey;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public RSAEncryptionProduct()
        {
            _encryptionType = EncryptionType.RSA;
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048); //2048 - Długość klucza
            _publicKey = cryptoServiceProvider.ExportParameters(false); //Generowanie klucza publiczny
            _privateKey = cryptoServiceProvider.ExportParameters(true); //Generowanie klucza prywatnego
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
            string publicKeyString = GetKeyString(_publicKey);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(data, false);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //rsa.PersistKeyInCsp = false;
                }
            }
        }
        public override string Decrypt(string text, string key)
        {
            var data = Convert.FromBase64String(text);
            var privateKeyString = GetKeyString(_privateKey);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKeyString);
                    var decryptedBytes = rsa.Decrypt(data, false);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                   // rsa.PersistKeyInCsp = false;
                }
            }
        }
        #endregion "PublicMethods"

        #region "Helper"
        private static string GetKeyString(RSAParameters publicKey)
        {

            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, publicKey);
            return stringWriter.ToString();
        }

        #endregion "Helper"
    }
}
