using static Kuro.PlayServe.Utilities.EnumTextFormat;

namespace Kuro.PlayServe.Utilities;

public static class EnumTextFormat
{
    public static readonly string NL = Environment.NewLine;
    public static readonly string TAB = "\t";
    public static readonly string TABTAB = "\t\t";
    public static readonly string NORMAL = IsOutputRedirected ? "" : "\x1b[39m";
    public static readonly string RED = IsOutputRedirected ? "" : "\x1b[91m";
    public static readonly string GREEN = IsOutputRedirected ? "" : "\x1b[92m";
    public static readonly string YELLOW = IsOutputRedirected ? "" : "\x1b[93m";
    public static readonly string BLUE = IsOutputRedirected ? "" : "\x1b[94m";
    public static readonly string MAGENTA = IsOutputRedirected ? "" : "\x1b[95m";
    public static readonly string CYAN = IsOutputRedirected ? "" : "\x1b[96m";
    public static readonly string GREY = IsOutputRedirected ? "" : "\x1b[97m";
    public static readonly string BOLD = IsOutputRedirected ? "" : "\x1b[1m";
    public static readonly string NOBOLD = IsOutputRedirected ? "" : "\x1b[22m";
    public static readonly string UNDERLINE = IsOutputRedirected ? "" : "\x1b[4m";
    public static readonly string NOUNDERLINE = IsOutputRedirected ? "" : "\x1b[24m";
    public static readonly string REVERSE = IsOutputRedirected ? "" : "\x1b[7m";
    public static readonly string NOREVERSE = IsOutputRedirected ? "" : "\x1b[27m";
}

public enum Operation // have no idea how to name this.
{
    None = 0,
    User = 2,  // Cyan
    Message = 4, // Blue
    Warning = 8, // Yellow
    Exception = 16, // Red
    Progress = 32, // Magenta
    Complete = 64 // Green
}

public static class OperationTextFormat
{
    public static readonly string NONE = $"{NORMAL}[{Operation.None}]{NORMAL}{TABTAB}";
    public static readonly string USER = $"{CYAN}[{Operation.User}]{NORMAL}{TABTAB}";
    public static readonly string MESSAGE = $"{BLUE}[{Operation.Message}]{NORMAL}{TABTAB}";
    public static readonly string WARNING = $"{YELLOW}[{Operation.Warning}]{NORMAL}{TABTAB}";
    public static readonly string EXCEPTION = $"{RED}[{Operation.Exception}]{NORMAL} {TABTAB}";
    public static readonly string PROGRESS = $"{MAGENTA}[{Operation.Progress}]{NORMAL} {TABTAB}";
    public static readonly string COMPLETE = $"{GREEN}[{Operation.Complete}]{NORMAL} {TABTAB}";
}

