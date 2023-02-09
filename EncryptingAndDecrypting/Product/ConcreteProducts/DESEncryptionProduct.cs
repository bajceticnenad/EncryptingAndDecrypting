﻿using EncryptingAndDecrypting.Creator;
using EncryptingAndDecrypting.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;

namespace EncryptingAndDecrypting.Product.ConcreteProducts
{
    /// <summary>  
    /// AES Encryption 'ConcreteProduct' class  
    /// </summary> 
    class DESEncryptionProduct : EncryptionProduct
    {
        #region "PrivateFields"
        private readonly EncryptionType _encryptionType;
        #endregion "PrivateFields"

        #region "PublicConstructor"
        public DESEncryptionProduct()
        {
            _encryptionType = EncryptionType.DES;
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
            byte[] encryptionKey = { }; //Encryption Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;

            try
            {
                encryptionKey = Encoding.UTF8.GetBytes(key);
                // DESCryptoServiceProvider is a cryptography class defind in c#.  
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(clearText);
                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(encryptionKey, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override string Decrypt(string cipherText, string key)
        {
            byte[] encryptionKey = { };// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray = new byte[cipherText.Length];

            try
            {
                encryptionKey = Encoding.UTF8.GetBytes(key);
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(cipherText);

                MemoryStream Objmst = new MemoryStream();
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(encryptionKey, IV), CryptoStreamMode.Write);
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);
                Objcs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(Objmst.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion "PublicMethods"
    }
}
