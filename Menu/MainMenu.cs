namespace FileEncryptor.Menu;

public class MainMenu : BaseMenu
{
    private string _fileName;
    
    public MainMenu(string fileName)
    {
        _fileName = fileName;
    }
    public override void Menu()
    {
        ConsoleHelpers.WriteWithColor("File: ", ConsoleColor.Yellow);
        Console.WriteLine($"{_fileName}\n");
        WriteMenuOption(1, "Encrypt file");
        WriteMenuOption(2, "Decrypt file");
        WriteMenuOption(3, "Change file");
        Console.WriteLine();
        if (Count > 1)
        {
            WriteMenuOption(9, "Back");
        }
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
                Encryptor.Encrypt(_fileName);
                ConsoleHelpers.EnterKeyToContinue();
                break;
            }

            case "2":
            {
                Encryptor.Decrypt(_fileName);
                ConsoleHelpers.EnterKeyToContinue();
                break;
            }

            case "3":
            {
                _fileName = ConsoleHelpers.GetFileName();
                break;
            }

            case "9":
            {
                if (Count > 1)
                {
                    Pop();
                }
                
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