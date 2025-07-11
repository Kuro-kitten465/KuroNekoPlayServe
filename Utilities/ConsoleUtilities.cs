using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Utilities
{
    public static class ConsoleUtilities
    {
        public static string GetInput(string message)
        {
            Write(message);
            string? input = ReadLine();
            if (string.IsNullOrEmpty(input)) input = "";
            return input;
        }

        public static void Exit()
        {
            WriteLine($"{NL}{BLUE}[{Operation.Message}]{NORMAL}{TABTAB}Press {GREEN}\"Enter\"{GREEN} to exit.");
            ReadKey();
            Environment.Exit(0);
        }
    }
}
