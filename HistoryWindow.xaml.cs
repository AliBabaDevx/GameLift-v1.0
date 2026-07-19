using System.Windows;

namespace GameLift;
public partial class HistoryWindow : Window
{
    private readonly AppSettings _settings;
    private readonly SettingsStore _store = new();
    public HistoryWindow(AppSettings settings) { InitializeComponent(); _settings = settings; Refresh(); }
    private void Refresh() => HistoryList.ItemsSource = _settings.Sessions;
    private void Clear_Click(object sender, RoutedEventArgs e) { _settings.Sessions.Clear(); _store.Save(_settings); Refresh(); }
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}
