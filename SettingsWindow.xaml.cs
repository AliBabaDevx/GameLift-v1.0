using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace GameLift;
public partial class SettingsWindow : Window
{
    private readonly AppSettings _settings;
    private readonly SettingsStore _store = new();
    public SettingsWindow(AppSettings settings) { InitializeComponent(); _settings = settings; GitHubBox.Text = settings.Social.GitHub; DiscordBox.Text = settings.Social.Discord; YouTubeBox.Text = settings.Social.YouTube; UpdateLanguage(); }
    private void LanguageButton_Click(object sender, RoutedEventArgs e) { _settings.Language = _settings.Language == "en" ? "de" : "en"; _store.Save(_settings); UpdateLanguage(); }
    private void UpdateLanguage() { var english = _settings.Language == "en"; LanguageText.Text = english ? "English" : "Deutsch"; LanguageButton.Content = english ? "Zu Deutsch" : "Zu Englisch"; }
    private void OpenFolder_Click(object sender, RoutedEventArgs e) { var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GameLift"); Directory.CreateDirectory(path); Process.Start(new ProcessStartInfo("explorer.exe", path) { UseShellExecute = true }); }
    private void SaveLinks_Click(object sender, RoutedEventArgs e)
    {
        if (!IsWebUrl(GitHubBox.Text) || !IsWebUrl(DiscordBox.Text) || !IsWebUrl(YouTubeBox.Text)) { MessageBox.Show("Bitte nutze vollständige https://-Links.", "GameLift", MessageBoxButton.OK, MessageBoxImage.Warning); return; }
        _settings.Social.GitHub = GitHubBox.Text.Trim(); _settings.Social.Discord = DiscordBox.Text.Trim(); _settings.Social.YouTube = YouTubeBox.Text.Trim(); _store.Save(_settings);
        MessageBox.Show("Community-Links gespeichert.", "GameLift", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    private void Backup_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog { Filter = "GameLift-Backup (*.json)|*.json", FileName = $"GameLift-backup-{DateTime.Now:yyyy-MM-dd}.json" };
        if (dialog.ShowDialog() == true) File.WriteAllText(dialog.FileName, JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true }));
    }
    private static bool IsWebUrl(string value) => Uri.TryCreate(value, UriKind.Absolute, out var uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp);
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
}
