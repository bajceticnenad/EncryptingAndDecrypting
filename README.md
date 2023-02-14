# EncryptingAndDecrypting
Nuget (C# library) for Encrypting and Decrypting based on AES, Triple DES, DES encryption

## Example

```csharp
       public void EncryptionTest()
        {
            var str = "TestString";
            var encrypt = EncryptionMethods.Instance.AES_Encrypt(str, "123");
            var decrypt = EncryptionMethods.Instance.AES_Decrypt(encrypt, "123");

        }
```

## Installation

` Install-Package EncryptingAndDecrypting `
