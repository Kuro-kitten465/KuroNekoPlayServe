using Kuro.PlayServe.Configs;
using Kuro.PlayServe.Menus;
using static Kuro.PlayServe.Utilities.ConsoleUtilities;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe;

internal class Program
{
    private static bool _isRunning = false;
    private static CancellationTokenSource _cts = new();

    private static Configurations _config = new();

    private static IMenuService? _menuService { get; set; }

    public static async Task Main(string[] args)
    {
        await Initialize();

        _menuService = new GameListMenu();
        await _menuService.OpenMenu();
        await Task.Delay(5000);
        _menuService = new ServerMenu();
        await _menuService.OpenMenu();

    }

    public static void SetUpConfig(Configurations config) =>
        _config = config;

    private static async Task Initialize()
    {
        _menuService = new ConfigMenu();
        await _menuService.OpenMenu();

        //Write(_config.AutoOpen.ToString() + _config.GameFolder + _config.Port);
    }
}
