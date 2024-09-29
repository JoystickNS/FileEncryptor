using System.Diagnostics.CodeAnalysis;
using Microsoft.Win32;

namespace FileEncryptor;

[SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы")]
public static class ContextMenu
{
    private const string KeyPath = @$"*\shell\{Global.AppKey}";
    private const string EncryptPath = @$"Software\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{Global.AppName}.Encrypt";
    private const string DecryptPath = @$"Software\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\shell\{Global.AppName}.Decrypt";
    
    public static void AddContextMenu()
    {
        using var key = Registry.ClassesRoot.CreateSubKey(KeyPath);
        key.SetValue("MUIVerb", Global.AppName);
        key.SetValue("Icon", Global.DestFileName);
        key.SetValue("SubCommands", $"{Global.AppName}.Encrypt;{Global.AppName}.Decrypt");

        using var encryptKey = Registry.LocalMachine.CreateSubKey(EncryptPath);
        encryptKey.SetValue("MUIVerb", "Encrypt");
        using var encryptKeyCommand = Registry.LocalMachine.CreateSubKey($@"{EncryptPath}\command");
        encryptKeyCommand.SetValue("", @$"{Global.DestFileName} -enc ""%1""");

        using var decryptKey = Registry.LocalMachine.CreateSubKey(DecryptPath);
        decryptKey.SetValue("MUIVerb", "Decrypt");
        using var decryptKeyCommand = Registry.LocalMachine.CreateSubKey($@"{DecryptPath}\command");
        decryptKeyCommand.SetValue("", @$"{Global.DestFileName} -dec ""%1""");
    }

    public static void RemoveContextMenu()
    {
        Registry.ClassesRoot.DeleteSubKeyTree(KeyPath, false);
        Registry.LocalMachine.DeleteSubKeyTree(EncryptPath, false);
        Registry.LocalMachine.DeleteSubKeyTree(DecryptPath, false);
    }
}