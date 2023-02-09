using System;
using System.Collections.Generic;
using System.Text;
using EncryptingAndDecrypting.Creator.ConcreteCreator;
using EncryptingAndDecrypting.Enums;
using EncryptingAndDecrypting.Product;
using static System.Net.Mime.MediaTypeNames;

namespace EncryptingAndDecrypting
{
    public sealed class EncryptionMethods
    {

        EncryptionMethods()
        {
        }
        private static readonly object padlock = new object();
        private static EncryptionMethods instance = null;
        public static EncryptionMethods Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EncryptionMethods();
                    }
                    return instance;
                }
            }
        }

        public string Encrypt(EncryptionType encryptionType, string text, string key)
        {
            try
            {
                return GetConcreteProduct(encryptionType).Encrypt(text, key);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Decrypt(EncryptionType encryptionType, string text, string key)
        {       
            try
            {
                return GetConcreteProduct(encryptionType).Decrypt(text, key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region "PrivateMethods"
        private EncryptionProduct GetConcreteProduct(EncryptionType encryptionType)
        {
            switch (encryptionType)
            {
                case EncryptionType.AES:
                    return new AESEncryptionFactory().GetEncryptionProduct();
                    //break;
                case EncryptionType.DES:
                    return new DESEncryptionFactory().GetEncryptionProduct();
                    //break;

                default:
                    return null;
                    //break;
            }
        }
        #endregion "PrivateMethods"
    }
}
