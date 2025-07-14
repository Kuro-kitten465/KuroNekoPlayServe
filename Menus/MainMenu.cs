using static Kuro.PlayServe.Utilities.ConsoleUtilities;
using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;
using Kuro.PlayServe.Cores;
using Kuro.PlayServe.Utilities;

namespace Kuro.PlayServe.Menus
{
    public class MainMenu() : IMenuService
    {
        private List<(string, string)> _games = [];

        public async Task OpenMenu()
        {
            await FindGamesMenu();

            if (_games.Count > 0)
                await GameSelectionMenu();
        }

        private async Task FindGamesMenu()
        {
            WriteLine($"{PROGRESS}Finding WebGL Games...{NL}");
            _games = FindWebGLGames(Application.Configurations.GameFolder);
            await Task.Delay(1000);

            if (_games.Count > 0)
            {
                foreach (var (name, path) in _games)
                {
                    WriteLine($"{MESSAGE}Found: {GREEN}{name}{NORMAL} at {BLUE}{path}{NORMAL}");
                }

                await Task.Delay(2000);
            }
            else
            {
                WriteLine($"{WARNING}Couldn't found any game...");
                WriteLine($"{WARNING}Pls Make sure you already put the game in the folder");
                WriteLine($"{WARNING}Or write a new game path in the Configs.txt");
                WriteLine($"{NL}{MESSAGE}Program will exit in 10 sec");
                await Task.Delay(10000);
                Environment.Exit(0);
            }
        }

        private static List<(string GameName, string GamePath)> FindWebGLGames(string rootFolder)
        {
            var result = new List<(string GameName, string GamePath)>();

            foreach (var dir in Directory.GetDirectories(rootFolder, "*", SearchOption.AllDirectories))
            {
                var indexPath = Path.Combine(dir, "index.html");
                if (File.Exists(indexPath))
                {
                    string gameName = Path.GetFileName(dir);
                    result.Add((gameName, dir));
                }
            }

            return result;
        }

        private async Task GameSelectionMenu()
        {
            WriteLine($"{MESSAGE}{NL}");
        }
    }
}
