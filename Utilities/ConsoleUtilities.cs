using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Utilities
{
    public static class ConsoleUtilities
    {
        public static string GetInput(string message)
        {
            WriteLine($"{MESSAGE}{message}");
            Write($"{USER}{TAB}");
            string? input = ReadLine();
            if (string.IsNullOrEmpty(input)) input = "";
            return input;
        }
    }
}
