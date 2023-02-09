using System;
using System.Collections.Generic;
using System.Text;
using EncryptingAndDecrypting.Creator.ConcreteCreator;
using EncryptingAndDecrypting.Enums;
using EncryptingAndDecrypting.Product;

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
            string result = null;
            switch (encryptionType)
            {
                case EncryptionType.AES:
                    result = new AESEncryptionFactory().GetEncryptionProduct().Encrypt(text, key);
                    break;
                case EncryptionType.DES:
                    result = new DESEncryptionFactory().GetEncryptionProduct().Encrypt(text, key);
                    break;

                default:
                    break;
            }

            return result;
        }
        public string Decrypt(EncryptionType encryptionType, string text, string key)
        {
            string result = null;
            switch (encryptionType)
            {
                case EncryptionType.AES:
                    result = new AESEncryptionFactory().GetEncryptionProduct().Decrypt(text, key);
                    break;
                case EncryptionType.DES:
                    result = new DESEncryptionFactory().GetEncryptionProduct().Decrypt(text, key);
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}
