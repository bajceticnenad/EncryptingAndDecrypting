using EncryptingAndDecrypting.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptingAndDecrypting.Product
{
    /// <summary>  
    /// EncryptionProduct - The 'Product' Abstract Class  
    /// </summary>  
    public abstract class EncryptionProduct
    {
        public abstract EncryptionType EncryptionType { get; }

        public abstract string Encrypt(string clearText, string key);
        public abstract string Decrypt(string cipherText, string key);
    }
}
