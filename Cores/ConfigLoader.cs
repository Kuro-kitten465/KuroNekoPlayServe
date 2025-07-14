using Kuro.PlayServe.Utilities;
using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Cores;

public struct Configurations
{
    public ushort Port { get; set; }
    public string GameFolder { get; set; }
    public bool AutoOpen { get; set; }
}

public sealed class ConfigLoader(string configPath) : IDisposable
{
    private readonly string _configPath = configPath;
    private readonly string[] _defaultConfigs = [
        "Port:8080",
        "GameFolder:Games",
        "AutoOpen:true"
    ];

    private bool disposedValue;

    public ConfigLoader LoadConfig(out Configurations configs)
    {
        if (!File.Exists(_configPath))
            CreateNewConfig();

        var temp = new Configurations();

        var lines = File.ReadAllLines(_configPath);

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line) || line.Contains("//")) continue;
            if (string.IsNullOrWhiteSpace(line) || !line.Contains(':')) continue;
            var parts = line.Trim().Split(':');

            try
            {
                if (parts[0].Equals("Port")) temp.Port = Convert.ToUInt16(parts[1]);
                if (parts[0].Equals("GameFolder"))
                {
                    temp.GameFolder = parts[1];

                    if (string.IsNullOrEmpty(temp.GameFolder) || !Directory.Exists(temp.GameFolder))
                        Directory.CreateDirectory(temp.GameFolder);
                }
                if (parts[0].Equals("AutoOpen")) temp.AutoOpen = Convert.ToBoolean(parts[1]);
            }
            catch (Exception)
            {
                WriteLine($"{RED}[{Operation.Exception}]{NORMAL}{TABTAB}{TAB}Can't load Config.txt file. We will use default values instead.");

                CreateNewConfig();

                temp.Port = 8080;
                temp.GameFolder = "Games";
                temp.AutoOpen = true;
            }
        }

        configs = temp;

        return this;
    }

    private void CreateNewConfig() =>
        File.WriteAllLines(_configPath, _defaultConfigs);

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
