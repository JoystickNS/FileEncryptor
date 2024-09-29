namespace FileEncryptor.Menu;

public abstract class BaseMenu
{
    private static readonly Stack<BaseMenu> MenuStack = new();
    private static string _errorText = "";

    public int Count => MenuStack.Count;
    
    public static void Run()
    {
        while (MenuStack.Count > 0)
        {
            var currentMenu = MenuStack.Peek();
            currentMenu.Display();
        }
    }

    protected static void SetError(string text)
    {
        _errorText = text;
    }
    
    protected static void WriteMenuOption(int number, string text)
    {
        ConsoleHelpers.WriteWithColor($"{number}. ", ConsoleColor.Red);
        Console.WriteLine(text);
    }
    
    public void Display()
    {
        Console.Clear();
        Global.WriteAppName();
        Menu();

        if (!string.IsNullOrEmpty(_errorText))
        {
            ConsoleHelpers.WriteError(_errorText);
            _errorText = "";
        };
        
        HandleInput();
    }

    public static void Push(BaseMenu menu)
    {
        MenuStack.Push(menu);
    }

    public static BaseMenu Peek()
    {
        return MenuStack.Peek();
    }

    public static BaseMenu Pop()
    {
        return MenuStack.Pop();
    }
    
    public abstract void Menu();
    public abstract void HandleInput();
}