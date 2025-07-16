using Kuro.PlayServe.Cores;
using Kuro.PlayServe.Utilities;
using static Kuro.PlayServe.Utilities.ConsoleUtilities;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe;

internal static class Program
{
    public static void Main()
    {
        ConsoleExtensions.EnableVirtualTerminalProcessing();
        WriteLine($"{MESSAGE}Starting KuroNekoPlayServe...");

        Application.Initialize();

        while (Application.IsRunning)
        {
            var input = GetInput($"Press Q to quit{NL}").ToLower();
            if (input == "q")
            {
                Application.Shutdown();
                break;
            }
        }
    }
}
