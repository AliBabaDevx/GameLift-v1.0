using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace GameLift;

public partial class PerformanceOverlay : Window
{
    private readonly Process _game;
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private TimeSpan _lastCpu;
    private DateTime _lastSample;

    public PerformanceOverlay(Process game, string gameName)
    {
        InitializeComponent();
        _game = game;
        GameNameText.Text = gameName;
        Left = SystemParameters.WorkArea.Right - Width - 22;
        Top = SystemParameters.WorkArea.Top + 22;
        _timer.Tick += (_, _) => UpdateMetrics();
        Loaded += (_, _) => { _lastSample = DateTime.UtcNow; _lastCpu = _game.TotalProcessorTime; _timer.Start(); };
        Closed += (_, _) => _timer.Stop();
    }

    private void UpdateMetrics()
    {
        try
        {
            if (_game.HasExited) { Close(); return; }
            _game.Refresh();
            var now = DateTime.UtcNow;
            var elapsed = (now - _lastSample).TotalSeconds;
            var cpu = ((_game.TotalProcessorTime - _lastCpu).TotalSeconds / elapsed) * 100 / Environment.ProcessorCount;
            _lastCpu = _game.TotalProcessorTime; _lastSample = now;
            CpuText.Text = $"CPU: {Math.Max(0, cpu):0.0}%";
            RamText.Text = $"RAM: {_game.WorkingSet64 / 1024d / 1024d:0} MB";
            FpsText.Text = "FPS: sichere Messung folgt";
        }
        catch { Close(); }
    }
}
