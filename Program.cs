using static Kuro.PlayServe.Utilities.ConsoleUtilities;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;
using Kuro.PlayServe.Cores;
using Kuro.PlayServe.Utilities;

namespace Kuro.PlayServe;

internal static class Program
{
    private static readonly Application _app = new();
    private static readonly InputSystem _inputSystem = new();

    public static void Main()
    {
        ConsoleExtensions.EnableVirtualTerminalProcessing();

        _inputSystem.ESCKeyPressed += () =>
        {
            _app.Stop();
        };

        _inputSystem.StartListening();

        _app.Run();

        Exit();
    }

    public static void Exit()
    {
        Clear();
        WriteLine($"{MESSAGE}Press {GREEN}\"Enter\"{GREEN} to exit.");
        ReadKey();
        Environment.Exit(0);
    }
}
