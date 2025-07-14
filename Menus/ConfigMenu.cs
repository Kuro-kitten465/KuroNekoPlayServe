using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;
using Kuro.PlayServe.Cores;

namespace Kuro.PlayServe.Menus
{
    public class ConfigMenu : IMenuService
    {
        public event Action<Configurations>? OnLoadingCompleted;

        public async Task OpenMenu()
        {
            WriteLine($"{PROGRESS}Initialize Configurations...");

            _ = new ConfigLoader("Configs.txt")
                .LoadConfig(out Configurations configs);

            await Task.Delay(1000);

            WriteLine($"{NL}{MESSAGE}Port: {GREEN}{configs.Port}");
            WriteLine($"{MESSAGE}Game Folder: {GREEN}{configs.GameFolder}");
            WriteLine($"{MESSAGE}Auto Open Game: {GREEN}{configs.AutoOpen}{NL}");

            OnLoadingCompleted?.Invoke(configs);

            WriteLine($"{COMPLETE}Initialize Complete!{NL}");
            await Task.Delay(2000);
        }
    }
}
