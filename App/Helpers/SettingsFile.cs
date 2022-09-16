using Hurl.BrowserSelector.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Hurl.BrowserSelector.Helpers
{
    public class SettingsFile
    {
        // convert this to reuable, so we can use it in other places
        // ex: SettingsFile(string filePath, DataModel)

        public Settings SettingsObject;

        private SettingsFile(Settings settings)
        {
            this.SettingsObject = settings;
        }

        public static SettingsFile TryLoading()
        {
            return new SettingsFile(GetSettings());
        }

        public static Settings GetSettings()
        {
            try
            {
                string jsondata = File.ReadAllText(Constants.SettingsFilePath);
                var SettingsObject = JsonSerializer.Deserialize<Settings>(jsondata);
                return SettingsObject;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case FileNotFoundException _:
                    case DirectoryNotFoundException _:
                        return New(GetBrowsers.FromRegistry()).SettingsObject;
                    default:
                        MessageBox.Show(e.Message, "ERROR");
                        throw;
                }
            }
        }

        public static SettingsFile New(List<Browser> browsers)
        {
            Directory.CreateDirectory(Constants.ROAMING + "\\Hurl");

            var _settings = new Settings()
            {
                Browsers = browsers,
            };

            string jsondata = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            });
            File.WriteAllText(Constants.SettingsFilePath, jsondata);

            return new SettingsFile(_settings);
        }

        public void Update()
        {
            SettingsObject.LastUpdated = DateTime.Now.ToString();
            string jsondata = JsonSerializer.Serialize(SettingsObject, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            });
            File.WriteAllText(Constants.SettingsFilePath, jsondata);
        }
    }
}
