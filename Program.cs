using FileEncryptor.Menu;

namespace FileEncryptor;

public static class Program
{
    private static void Main(string[] args)
    {
        Console.Title = Global.AppName;
        
        try
        {
            switch (args.Length)
            {
                case 0:
                {
                    var startMenu = new StartMenu();
                    BaseMenu.Push(startMenu);
                    BaseMenu.Run();
                    break;
                }
                
                case 1:
                {
                    var mainMenu = new MainMenu(args[0]);
                    BaseMenu.Push(mainMenu);
                    BaseMenu.Run();
                    break;
                }
                
                case 2:
                {
                    var command = args[0];
                    var filename = args[1];

                    switch (command)
                    {
                        case "-enc":
                        {
                            Global.WriteAppName();
                            Encryptor.Encrypt(filename);
                            break;
                        }

                        case "-dec":
                        {
                            Global.WriteAppName();
                            Encryptor.Decrypt(filename);
                            break;
                        }

                        default:
                        {
                            ConsoleHelpers.WriteError("Unknown command: " + command);
                            break;
                        }
                    }
                    
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            ConsoleHelpers.WriteError("Error: ");
            Console.WriteLine(ex.Message);
        }
        
        ConsoleHelpers.EnterKeyToContinue();
    }
}
