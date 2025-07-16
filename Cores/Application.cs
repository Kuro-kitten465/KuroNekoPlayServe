using static Kuro.PlayServe.Utilities.OperationTextFormat;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Cores
{
    internal class Application
    {
        private static Server? _server;
        public static bool IsRunning { get; private set; } = true;

        public static Configurations Config { get; private set; } = new Configurations
        {
            GameFolder = string.Empty,
            Port = 8080,
            AutoOpen = true
        };

        public static void Initialize()
        {
            Config = ConfigurationsManager.ConfigsLoader("Configs.txt");
            WriteLine($"{NL}{COMPLETE}Configurations loaded!{NL}");
            WriteLine($"{MESSAGE}GameFolder:{TAB}{GREEN}{Config.GameFolder}");
            WriteLine($"{MESSAGE}Port:{TABTAB}{GREEN}{Config.Port}");
            WriteLine($"{MESSAGE}AutoOpen:{TAB}{GREEN}{Config.AutoOpen}{NL}");

            var gamePath = GameFinder();
            if (string.IsNullOrEmpty(gamePath))
            {
                WriteLine($"{WARNING}No WebGL game found. Please place your game in the {Config.GameFolder} folder.");
                return;
            }

            _server = new Server(Config.Port, gamePath);
            _server.Start();

            if (Config.AutoOpen)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = $"http://localhost:{Config.Port}",
                    UseShellExecute = true
                });
            }
        }

        public static void Shutdown()
        {
            IsRunning = false;
            _server?.Stop();
            WriteLine($"{COMPLETE}Server stopped. Thank you for using KuroNekoPlayServe!");
            WriteLine($"{COMPLETE}Press Enter to exit.");
            ReadKey();
        }

        private static string GameFinder()
        {
            if (!Directory.Exists(Config.GameFolder))
            {
                Directory.CreateDirectory(Config.GameFolder);
                WriteLine($"{PROGRESS}Created game folder at {Config.GameFolder}");
                return string.Empty;
            }

            WriteLine($"{PROGRESS}Searching for WebGL game...");
            var indexFiles = Directory.GetFiles(Config.GameFolder, "index.html", SearchOption.AllDirectories);
            
            if (indexFiles.Length == 0)
            {
                WriteLine($"{WARNING}No index.html found in {Config.GameFolder}");
                return string.Empty;
            }

            if (indexFiles.Length > 1)
            {
                WriteLine($"{WARNING}Multiple index.html files found. Using the first one:");
                foreach (var file in indexFiles)
                {
                    WriteLine($"{PROGRESS}Found: {file}");
                }
            }

            WriteLine($"{COMPLETE}Found game at: {Path.GetDirectoryName(indexFiles[0])}");
            return Path.GetDirectoryName(indexFiles[0]) ?? string.Empty;
        }
    }
}
