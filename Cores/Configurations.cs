using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuro.PlayServe.Cores
{
    public struct Configurations
    {
        public string GameFolder;
        public ushort Port;
        public bool AutoOpen;
    }

    public static class ConfigurationsManager
    {
        public static Configurations ConfigsLoader(string path)
        {
            if (!File.Exists(path))
            {
                var defaultConfig = new Configurations
                {
                    GameFolder = "Game",
                    Port = 8080,
                    AutoOpen = true
                };

                SaveConfigurations(path, defaultConfig);
                return defaultConfig;
            }

            var configLines = File.ReadAllLines(path);
            var config = new Configurations();
            foreach (var line in configLines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split('=');
                if (parts.Length != 2)
                    continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                switch (key)
                {
                    case "GameFolder":
                        config.GameFolder = value;
                        break;
                    case "Port":
                        if (ushort.TryParse(value, out var port))
                            config.Port = port;
                        break;
                    case "AutoOpen":
                        if (bool.TryParse(value, out var autoOpen))
                            config.AutoOpen = autoOpen;
                        break;
                }
            }

            return config;
        }

        private static void SaveConfigurations(string path, Configurations defaultConfig)
        {
            using var writer = new StreamWriter(path);
            writer.WriteLine($"GameFolder={defaultConfig.GameFolder}");
            writer.WriteLine($"Port={defaultConfig.Port}");
            writer.WriteLine($"AutoOpen={defaultConfig.AutoOpen}");
        }
    }
}
