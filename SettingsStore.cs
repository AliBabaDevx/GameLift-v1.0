using System.Text.Json;
using System.IO;

namespace GameLift;
public sealed class AppSettings
{
    public List<GameProfile> Profiles { get; set; } = [];
    public List<GameSession> Sessions { get; set; } = [];
    public SocialLinks Social { get; set; } = new();
    public string Language { get; set; } = "de";
}

public sealed class SocialLinks
{
    public string GitHub { get; set; } = "https://github.com";
    public string Discord { get; set; } = "https://discord.com";
    public string YouTube { get; set; } = "https://youtube.com";
}
public sealed class SettingsStore
{
    private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GameLift", "profiles.json");
    public AppSettings Load()
    {
        try { return File.Exists(_path) ? JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_path)) ?? new() : new(); }
        catch { return new(); }
    }
    public void Save(AppSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        File.WriteAllText(_path, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
    }
}
