using Kuro.PlayServe.Utilities;
using static Kuro.PlayServe.Utilities.EnumTextFormat;
using static Kuro.PlayServe.Utilities.ConsoleUtilities;

namespace Kuro.PlayServe.Configs;

public struct Configurations
{
    public ushort Port { get; set; }
    public string? GameFolder { get; set; }
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
                        throw new FormatException();
                }
                if (parts[0].Equals("AutoOpen")) temp.AutoOpen = Convert.ToBoolean(parts[1]);
            }
            catch (FormatException)
            {
                WriteLine($"{RED}[{Operation.Error}]{NORMAL}{TABTAB}{TAB}Can't load Config.txt file. Due to Format isn't right.");
                WriteLine($"{BLUE}[{Operation.Message}]{NORMAL}{TABTAB}Pls try to remove the file or set a new value.");
                Exit();
            }
            catch (OverflowException)
            {
                WriteLine($"{RED}[{Operation.Error}]{NORMAL}{TABTAB}{TAB}Can't load Config.txt file. Due to value isn't valid.");
                WriteLine($"{BLUE}[{Operation.Message}]{NORMAL}{TABTAB}Pls try to remove the file or set a new value.");
                Exit();
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
