using EncryptingAndDecrypting.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptingAndDecrypting.Creator
{
    /// <summary>  
    /// The 'Creator' Abstract Class  
    /// </summary>  
    abstract class EncryptionFactory
    {
        public abstract EncryptionProduct GetEncryptionProduct();
    }
}
