namespace FileEncryptor.Menu;

public class ConfirmMenu : BaseMenu
{
    private readonly Action _action;
    private readonly string _text;
    
    public ConfirmMenu(Action action, string text = "")
    {
        _action = action;
        _text = text;
    }
    
    public override void Menu()
    {
        if (!string.IsNullOrEmpty(_text))
        {
            Console.WriteLine(_text);
        }
        
        Console.WriteLine("Continue? (y/n)");
    }

    public override void HandleInput()
    {
        string? input;
        do
        {
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) break;
            
        } while (true);
        
        switch (input)
        {
            case "y":
            {
                _action();
                Pop();
                break;
            }

            case "n":
            {
                Pop();
                break;
            }
        }
    }
}