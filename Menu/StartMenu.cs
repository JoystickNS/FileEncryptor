namespace FileEncryptor.Menu;

public class StartMenu : BaseMenu
{
    public override void Menu()
    {
        WriteMenuOption(1, "Encrypt/Decrypt file");
        WriteMenuOption(2, (Installer.IsInstalled() ? "Uninstall App" : "Install App") + " (context menu)");
        Console.WriteLine();
        WriteMenuOption(0, "Close program");
        Console.WriteLine();
    }

    public override void HandleInput()
    {
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
            {
                Push(new MainMenu(ConsoleHelpers.GetFileName()));
                break;
            }

            case "2":
            {
                if (!Installer.IsRunningAsAdministrator())
                {
                    SetError("You must run the program as an administrator");
                    break;
                }

                Push(Installer.IsInstalled()
                    ? new ConfirmMenu(Installer.UninstallProgram, "Are you going to delete the installed program")
                    : new ConfirmMenu(Installer.InstallProgram,
                        $"The program will be installed in the folder {Global.InstallPath}"));
                break;
            }

            case "0":
            {
                Environment.Exit(0);
                break;
            }
            
            default:
            {
                SetError("Invalid option. Please try again.");
                break;
            }
        }
    }
}