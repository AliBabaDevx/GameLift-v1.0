using System.Net.NetworkInformation;

namespace GameLift;

public sealed class NetworkService
{
    public async Task<string> TestAsync()
    {
        using var ping = new Ping();
        var result = await ping.SendPingAsync("1.1.1.1", 3000);
        return result.Status == IPStatus.Success
            ? $"{result.RoundtripTime} ms · Verbindung stabil"
            : $"{result.Status} · keine Ping-Antwort";
    }
}
