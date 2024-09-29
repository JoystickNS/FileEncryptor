namespace FileEncryptor;

public static class FileHelpers
{
    public static string GetUniqueFileName(string sourceFileName, string partName = "")
    {
        if (!File.Exists(sourceFileName))
        {
            throw new ArgumentException($"File ${sourceFileName} does not exists");
        }
        
        var directory = Path.GetDirectoryName(sourceFileName);
        if (string.IsNullOrEmpty(directory))
        {
            throw new ArgumentException($"Can't get the file directory {sourceFileName}");
        }
        
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFileName);
        var extension = Path.GetExtension(sourceFileName);

        while (true)
        {
            var newFileName = "";
            var guid = Guid.NewGuid().ToString();
            newFileName = string.IsNullOrEmpty(partName) ? $"{fileNameWithoutExtension}_{guid}" : $"{fileNameWithoutExtension}_{partName}_{guid}";
            newFileName = $"{Path.Combine(directory, newFileName)}{extension}";
            if (!File.Exists(newFileName)) return newFileName;
        }
    }
    
    public static string GetNewFileName(string sourceFileName, string partName = "")
    {
        var path = Path.GetDirectoryName(sourceFileName);
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException($"Can't get the file directory {sourceFileName}");
        }
        
        Console.WriteLine("Enter the file name or leave it blank to generate the file name automatically");

        var ext = Path.GetExtension(sourceFileName);
        var newFileName = Console.ReadLine();
        while (File.Exists(Path.Combine(path, newFileName ?? "")))
        {
            ConsoleHelpers.WriteError("А file with that name already exists");
            newFileName = Console.ReadLine();
        }
        
        return string.IsNullOrEmpty(newFileName) ? GetUniqueFileName(sourceFileName, partName) : $"{newFileName}{ext}";
    }
}