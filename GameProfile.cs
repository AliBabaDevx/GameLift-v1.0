using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace GameLift;
public sealed class GameProfile : INotifyPropertyChanged
{
    private string _name = "Neues Spiel";
    private bool _isFavorite;
    public event PropertyChangedEventHandler? PropertyChanged;
    public string Name { get => _name; set { _name = value; Notify(); Notify(nameof(DisplayName)); } }
    [JsonIgnore]
    public string DisplayName => IsFavorite ? $"★  {Name}" : Name;
    public string ExecutablePath { get; set; } = "";
    public string ProcessesToClose { get; set; } = "";
    public string LaunchArguments { get; set; } = "";
    public string Category { get; set; } = "General";
    public bool IsFavorite { get => _isFavorite; set { _isFavorite = value; Notify(); Notify(nameof(DisplayName)); } }
    public bool UseHighPerformancePlan { get; set; } = true;
    public bool ShowPerformanceOverlay { get; set; } = true;
    public int LaunchCount { get; set; }
    public DateTime? LastLaunchedUtc { get; set; }
    private void Notify([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
