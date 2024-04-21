using System.Security.Cryptography;
using System.Text;

namespace FitByBitApiService.Helpers;

public static class CommonEncryptionHandler
{
    public static string Encrypt(this string data, string key)
    {
        string encData = null!;
        byte[][] keys = GetHashKeys(key);
        encData = EncryptStringToBytes_Aes(data, keys[0], keys[1]);
        return encData;
    }

    public static string Decrypt(string data, string key)
    {
        string decData = null!;
        byte[][] keys = GetHashKeys(key);

        try
        {
            decData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
        }

        catch (CryptographicException) { }
        catch (ArgumentNullException) { }

        return decData;
    }

    private static byte[][] GetHashKeys(string key)
    {
        IConfiguration configuration = new ConfigurationManager();
        string secretKey = configuration["OtpEncryptionSettings:secretKey"]!;

        byte[][] result = new byte[2][];
        Encoding enc = Encoding.UTF8;

        SHA256 sha2 = new SHA256CryptoServiceProvider();

        byte[] rawKey = enc.GetBytes(key);
        byte[] rawIv = enc.GetBytes(key);

        byte[] hashKey = sha2.ComputeHash(rawKey);
        byte[] hashIv = sha2.ComputeHash(rawIv);

        Array.Resize(ref hashIv, 16);

        result[0] = hashKey;
        result[1] = hashIv;

        return result;
    }

    private static string EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException(nameof(plainText));
        if (key == null || key.Length <= 0)
            throw new ArgumentNullException(nameof(key));
        if (iV == null || iV.Length <= 0)
            throw new ArgumentNullException(nameof(iV));

        byte[] encrypted;

        using (var aesAlgorithm = Aes.Create())
        {
            aesAlgorithm.Key = key;
            aesAlgorithm.IV = iV;

            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                       new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encrypted);
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
                        // Read the decrypted bytes from the decrypting stream and place them in a string.                             
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
}
