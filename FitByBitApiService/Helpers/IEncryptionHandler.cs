namespace FitByBitService.Helpers;

public interface IEncryptionHandler
{
    string AesDecryptWithVector(string ciphertext);
    string AesEncryptWithVector(dynamic toBeEncrypted);
    dynamic DecryptStringAes(string cipherText);
    byte[] EncryptStringToBytes(dynamic toBeEncrypted);
}
