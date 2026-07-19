using System.Text.Json.Serialization;

namespace GameLift;
public sealed class GameSession
{
    public string GameName { get; set; } = "";
    public DateTime StartedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? EndedUtc { get; set; }
    [JsonIgnore]
    public string Summary
    {
        get
        {
            var duration = EndedUtc is null ? "läuft" : $"{Math.Max(0, (EndedUtc.Value - StartedUtc).TotalMinutes):0} Min.";
            return $"{GameName} · {StartedUtc.ToLocalTime():dd.MM. HH:mm} · {duration}";
        }
    }
}
