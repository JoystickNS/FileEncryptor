using System.Security.Cryptography;

namespace FileEncryptor;

public static class Encryptor
{
    public static void Encrypt(string fileName)
    {
        Console.WriteLine("Encrypting file...");
        var newFileName = FileHelpers.GetNewFileName(fileName, "enc");
        EncryptFile(fileName, newFileName, PasswordReader.GetPassword());
        ConsoleHelpers.WriteLineWithColor($"File encrypted successfully. File: {Path.GetFileName(newFileName)}", ConsoleColor.Green);
    }
    
    public static void Decrypt(string fileName)
    {
        Console.WriteLine("Decrypting file...");
        var newFileName = FileHelpers.GetNewFileName(fileName);
        while (true)
        {
            try
            {
                DecryptFile(fileName, newFileName, PasswordReader.GetPassword(false));
                ConsoleHelpers.WriteLineWithColor($"File decrypted successfully. File: {Path.GetFileName(newFileName)}", ConsoleColor.Green);
                break;
            }
            catch
            {
                ConsoleHelpers.WriteError("Invalid password or data has been changed");
            }
        }
    }
    
    private static void EncryptFile(string inputFilePath, string outputFilePath, string password)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);

        var key = GenerateKey(password, salt);
        var tagSize = AesGcm.TagByteSizes.MaxSize;
        var nonceSize = AesGcm.NonceByteSizes.MaxSize;
        
        var iv = new byte[nonceSize];
        RandomNumberGenerator.Fill(iv);
        
        var plaintext = File.ReadAllBytes(inputFilePath);
        var ciphertext = new byte[plaintext.Length];
        var tag = new byte[tagSize];
        
        using var aesGcm = new AesGcm(key, tagSize);
        aesGcm.Encrypt(iv, plaintext, ciphertext, tag);
        
        using var fsOutput = new FileStream(outputFilePath, FileMode.Create);
        fsOutput.Write(ciphertext, 0, ciphertext.Length);
        fsOutput.Write(salt, 0, salt.Length);
        fsOutput.Write(iv, 0, iv.Length);
        fsOutput.Write(tag, 0, tag.Length);
    }
    
    private static void DecryptFile(string inputFilePath, string outputFilePath, string password)
    {
        using var fsInput = new FileStream(inputFilePath, FileMode.Open);
        var fileLength = fsInput.Length;
        var tagSize = AesGcm.TagByteSizes.MaxSize;
        var nonceSize = AesGcm.NonceByteSizes.MaxSize;
        var salt = new byte[16];
        var iv = new byte[nonceSize];
        var tag = new byte[tagSize];
        
        fsInput.Position = fileLength - (salt.Length + nonceSize + tagSize);
        _ = fsInput.Read(salt, 0, salt.Length); 
        _ = fsInput.Read(iv, 0, iv.Length);     
        _ = fsInput.Read(tag, 0, tag.Length);
        
        var key = GenerateKey(password, salt);
        using var aesGcm = new AesGcm(key, tagSize);
        
        fsInput.Position = 0;
        var ciphertext = new byte[fileLength - (salt.Length + nonceSize + tagSize)];
        _ = fsInput.Read(ciphertext, 0, ciphertext.Length);

        var plaintext = new byte[ciphertext.Length];
        aesGcm.Decrypt(iv, ciphertext, tag, plaintext);
        File.WriteAllBytes(outputFilePath, plaintext);
    }
    
    private static byte[] GenerateKey(string password, byte[] salt)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        return rfc2898DeriveBytes.GetBytes(32);
    }
}