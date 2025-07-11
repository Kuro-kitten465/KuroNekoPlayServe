using Kuro.PlayServe.Configs;
using static Kuro.PlayServe.Utilities.ConsoleUtilities;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Menus
{
    public class ConfigMenu : IMenuService
    {
        public async Task OpenMenu()
        {
            WriteLine($"{PROGRESS}Initialize Configurations...");

            _ = new ConfigLoader("Configs.txt")
                .LoadConfig(out Configurations temp);

            await Task.Delay(1000);

            WriteLine($"{NL}{MESSAGE}Port: {GREEN}{temp.Port}");
            WriteLine($"{MESSAGE}Game Folder: {GREEN}{temp.GameFolder}");
            WriteLine($"{MESSAGE}Auto Open Game: {GREEN}{temp.AutoOpen}{NL}");

            WriteLine($"{COMPLETE}Initialize Complete!{NL}");
            GetInput($"{MESSAGE}Press {GREEN}\"Enter\"{NORMAL} to continue.{NL}");
            Clear();
            Program.SetUpConfig(temp);
        }
    }
}
