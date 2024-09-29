namespace FileEncryptor;

public static class ConsoleHelpers
{
    public static void WriteError(string text)
    {
        WriteLineWithColor(text, ConsoleColor.Red);
    }
    
    public static void WriteWithColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
    
    public static void WriteLineWithColor(string text, ConsoleColor color)
    {
        WriteWithColor($"{text}\n", color);
    }

    public static void EnterKeyToContinue()
    {
        WriteLineWithColor("Enter any key to continue", ConsoleColor.Magenta);
        Console.ReadKey();
    }

    public static string GetFileName()
    {
        Console.WriteLine("Paste the full path to the file or move the file to this window or install the application and use the context menu");
        string? fileName;
        do
        {
            WriteWithColor("File: ", ConsoleColor.Yellow);
            fileName = Console.ReadLine();
            if (File.Exists(fileName)) break;
            WriteError("File does not exist");
        } while (true);

        return fileName;
    }
}