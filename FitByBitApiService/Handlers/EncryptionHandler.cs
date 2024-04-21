using System.Security.Cryptography;
using System.Text;
using Aes = System.Security.Cryptography.Aes;

namespace FitByBitApiService.Handlers;

public class EncryptionHandler : IEncryptionHandler
{
    private readonly IConfiguration _config;

    public EncryptionHandler(IConfiguration config)
    {
        _config = config.GetSection("EncryptionSettings");
    }

    public byte[] EncryptStringToBytes(dynamic toBeEncrypted)
    {
        var secretKey = Encoding.UTF8.GetBytes(_config["SecretKey"]!);
        var ivKey = Encoding.UTF8.GetBytes(_config["IvKey"]!);
        string plainText = Convert.ToString(toBeEncrypted);

        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        if (ivKey == null || ivKey.Length <= 0)
        {
            throw new ArgumentNullException(nameof(secretKey));
        }
        byte[] encrypted;

        // Create a RijndaelManaged object with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;
            rijAlg.BlockSize = 128;
            rijAlg.Key = secretKey;
            rijAlg.IV = ivKey;

            // Create a decryptor to perform the stream transform.
            var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    var result = msEncrypt;
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private string DecryptStringFromBytes(byte[] cipherText)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException(nameof(cipherText));
        }
        var key = Encoding.ASCII.GetBytes(_config["SecretKey"]!);
        var iv = Encoding.ASCII.GetBytes(_config["IvKey"]!);

        // Declare the string used to hold the decrypted text.
        string plaintext = null!;

        // Create an RijndaelManaged object with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;
            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            try
            {
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }
        return plaintext;
    }

    public dynamic DecryptStringAes(string cipherText)
    {
        var encrypted = Convert.FromBase64String(cipherText);
        var decryptedFromJavascript = DecryptStringFromBytes(encrypted);
        return decryptedFromJavascript;
        //return JsonConvert.SerializeObject( decryptedFromJavascript);
    }

    public string AesEncryptWithVector(dynamic toBeEncrypted)
    {
        try
        {
            using (Aes myAes = Aes.Create())
            {
                string plainText = Convert.ToString(toBeEncrypted);
                myAes.Key = Encoding.UTF8.GetBytes(_config["SecretKey"]!);
                myAes.IV = Encoding.UTF8.GetBytes(_config["IvKey"]!);

                // Encrypt the string to an array of bytes.                     
                byte[] encrypted = EncryptStringToBytes_Aes(plainText, myAes.Key, myAes.IV);

                string ciphertext = ByteArrayToString(encrypted);

                return ciphertext;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string AesDecryptWithVector(string ciphertext)
    {
        try
        {
            // Create a new instance of the Aes class.  This generates a new key and initialization vector (IV).
            using (Aes myAes = Aes.Create())
            {
                myAes.Key = Encoding.UTF8.GetBytes(_config["SecretKey"]!);
                myAes.IV = Encoding.UTF8.GetBytes(_config["IvKey"]!);

                // Decrypt the bytes to a string.
                string roundtrip = DecryptStringFromBytes_Aes(ciphertext, myAes.Key, myAes.IV);
                return roundtrip;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] secretKey, byte[] iVKey)
    {
        // Check arguments.             
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));

        if (secretKey == null || secretKey.Length <= 0)
            throw new ArgumentNullException(nameof(secretKey));

        if (iVKey == null || iVKey.Length <= 0)
            throw new ArgumentNullException(nameof(iVKey));

        byte[] encrypted;

        // with the specified key and IV.             
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = secretKey;
            aesAlg.IV = iVKey;

            // Create an encryptor to perform the stream transform.                 
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.                 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.     
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.            
        return encrypted;
    }

    private static string DecryptStringFromBytes_Aes(string cipherText, byte[] secretKey, byte[] iVKey)
    {
        // Check arguments.             
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException(nameof(cipherText));

        if (secretKey == null || secretKey.Length <= 0)
            throw new ArgumentNullException(nameof(secretKey));

        if (iVKey == null || iVKey.Length <= 0)
            throw new ArgumentNullException(nameof(iVKey));

        // Declare the string used to hold the decrypted text.             
        string plaintext = null!;

        // Create an Aes object with the specified key and IV.             
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = secretKey;
            aesAlg.IV = iVKey;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] cipherbytes = HexadecimalStringToByteArray(cipherText);

            // Create the streams used for decryption.                 
            using (MemoryStream msDecrypt = new MemoryStream(cipherbytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream                             
                        // and place them in a string.                             
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }

    private static byte[] HexadecimalStringToByteArray(string input)
    {
        var outputLength = input.Length / 2;
        var output = new byte[outputLength];
        using (var sr = new StringReader(input))
        {
            for (var i = 0; i < outputLength; i++)
                output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
        }
        return output;
    }

    private static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        return hex.ToString();
    }
}