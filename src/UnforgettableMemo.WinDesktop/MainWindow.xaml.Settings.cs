using System.IO;
using System.Text.Json;

namespace UnforgettableMemo.WinDesktop
{
    public partial class MainWindow
    {
        private readonly MainWindowSettings mainWindowSettings;

        public class MainWindowSettings
        {
            public double? TopLocation { get; set; }
            public double? LeftLocation { get; set; }
            public bool IsPreemptive { get; set; } = true;
            public double RetrievabilityThreshold { get; set; } = 0.9;
        }

        private MainWindowSettings LoadSettings()
        {
            string filePath = System.IO.Path.Combine(this.settingsDirectory, this.settingsFilename);
            if (!File.Exists(filePath))
            {
                return new MainWindowSettings();
            }
            string fileText = File.ReadAllText(filePath);
            MainWindowSettings settings = JsonSerializer.Deserialize<MainWindowSettings>(fileText);
            return settings;
        }

        public void SaveSettings(MainWindowSettings settings)
        {
            Directory.CreateDirectory(this.settingsDirectory);
            string fileText = JsonSerializer.Serialize(settings, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(System.IO.Path.Combine(this.settingsDirectory, this.settingsFilename), fileText);
        }
    }
}
