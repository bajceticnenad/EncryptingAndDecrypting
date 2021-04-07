using System;
using System.Collections.Generic;
using System.Text;
using EncryptingAndDecrypting.Encription;

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

        public string AES_Encrypt(string text, string key)
        {
            return AESEncryption.Encrypt(text, key);
        }
        public string AES_Decrypt(string text, string key)
        {
            return AESEncryption.Decrypt(text, key);
        }
    }
}
