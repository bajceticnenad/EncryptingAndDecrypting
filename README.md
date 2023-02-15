# EncryptingAndDecrypting
Nuget (C# library) for Encrypting and Decrypting based on AES, Triple DES, DES encryption

## Example

```csharp
       public void EncryptionTest()
        {
            var str = "TestString";
            var encrypt = EncryptionMethods.Instance.Encrypt(EncryptingAndDecrypting.Enums.EncryptionType.AES, str, "123");
            var decrypt = EncryptionMethods.Instance.Decrypt(EncryptingAndDecrypting.Enums.EncryptionType.AES, encrypt, "123");

        }
```

## Installation

` Install-Package EncryptingAndDecrypting `
