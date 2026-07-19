using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GameLift;
public sealed class PowerPlanService
{
    public string? GetActiveScheme()
    {
        var text = Run("/getactivescheme");
        return Regex.Match(text, @"[0-9a-fA-F]{8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}").Value is var id && id.Length > 0 ? id : null;
    }
    public void SetHighPerformance() => Run("/setactive SCHEME_MIN");
    public void SetScheme(string id) => Run($"/setactive {id}");
    private static string Run(string arguments)
    {
        using var process = Process.Start(new ProcessStartInfo("powercfg.exe", arguments) { RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true });
        if (process is null) throw new InvalidOperationException("powercfg.exe konnte nicht gestartet werden.");
        var output = process.StandardOutput.ReadToEnd(); process.WaitForExit();
        if (process.ExitCode != 0) throw new InvalidOperationException("Windows konnte das Energieschema nicht ändern.");
        return output;
    }
}
