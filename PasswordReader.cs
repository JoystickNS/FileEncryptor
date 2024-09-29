using System.Text;

namespace FileEncryptor;

public static class PasswordReader
{
    public static string GetPassword(bool withConfirmation = true)
    {
        while (true)
        {
            Console.Write("Enter password: ");
            var password = ReadPassword();

            if (!withConfirmation) return password;
            
            Console.Write("Confirm password: ");
            var confirmPassword = ReadPassword();

            if (password == confirmPassword)
            {
                return password;
            }

            ConsoleHelpers.WriteError("Passwords do not match. Please try again.");
        }
    }

    private static string ReadPassword()
    {
        var passwordBuilder = new StringBuilder();
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (passwordBuilder.Length <= 0) continue;
                passwordBuilder.Length--;
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Enter)
            {
                passwordBuilder.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return passwordBuilder.ToString();
    }
}