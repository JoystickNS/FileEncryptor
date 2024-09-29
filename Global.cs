namespace FileEncryptor;

public static class Global
{
    public const string AppName = "FileEncryptor";
    public const string AppKey = $"{AppName} d980c036-8300-4f52-bbb8-7e7535af1a4e";
    public static readonly string InstallPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), AppName);
    public static readonly string DestFileName = Path.Combine(InstallPath, $"{AppName}.exe");
    
    public static void WriteAppName()
    {
        Console.WriteLine();
        ConsoleHelpers.WriteLineWithColor("""
                                          '||''''|  ||  '||             '||''''|                                               .                   
                                           ||  .   ...   ||    ....      ||  .    .. ...     ....  ... ..  .... ... ... ...  .||.    ...   ... ..  
                                           ||''|    ||   ||  .|...||     ||''|     ||  ||  .|   ''  ||' ''  '|.  |   ||'  ||  ||   .|  '|.  ||' '' 
                                           ||       ||   ||  ||          ||        ||  ||  ||       ||       '|.|    ||    |  ||   ||   ||  ||     
                                          .||.     .||. .||.  '|...'    .||.....| .||. ||.  '|...' .||.       '|     ||...'   '|.'  '|..|' .||.    
                                                                                                           .. |      ||                            
                                                                                                            ''      ''''                           
                                          """, ConsoleColor.Green);
        Console.WriteLine();
    }
}