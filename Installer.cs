using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using Microsoft.Win32;

namespace FileEncryptor;

[SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы")]
public static class Installer
{
    private const string SubKey = @$"Software\{Global.AppKey}";
    
    public static bool IsInstalled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(SubKey);
        return key != null;
    }

    public static void InstallProgram()
    {
        if (!Directory.Exists(Global.InstallPath))
        {
            Directory.CreateDirectory(Global.InstallPath);
        }

        if (Directory.Exists(Global.InstallPath) && Directory.GetFiles(Global.InstallPath).Length != 0)
        {
            throw new Exception($"The program could not be installed, the {Global.InstallPath} folder already exists");
        }
        
        File.Copy(Path.Combine(Directory.GetCurrentDirectory(), $"{Global.AppName}.exe"), Global.DestFileName);
        
        ContextMenu.AddContextMenu();
        
        using var key = Registry.CurrentUser.CreateSubKey($"Software\\{Global.AppKey}");
        key.SetValue("Installed", "True");
        
        ConsoleHelpers.WriteLineWithColor("The program has been successfully installed", ConsoleColor.Green);
        ConsoleHelpers.EnterKeyToContinue();
        Environment.Exit(0);
    }

    public static void UninstallProgram()
    {
        ContextMenu.RemoveContextMenu();
        Registry.CurrentUser.DeleteSubKey(SubKey, false);
        
        ConsoleHelpers.WriteLineWithColor("The program was successfully deleted", ConsoleColor.Green);
        ConsoleHelpers.EnterKeyToContinue();

        var processInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/C timeout 2 & rmdir /S /Q \"{Global.InstallPath}\"",
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            Verb = "runas",
        };

        Process.Start(processInfo);
        
        Environment.Exit(0);
    }
    
    public static bool IsRunningAsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}