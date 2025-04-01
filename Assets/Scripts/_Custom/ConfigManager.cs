using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityCommunity.UnitySingleton;

namespace _Custom
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        private Dictionary<string, int> configValues = new Dictionary<string, int>();
        private const string CONFIG_PATH = "Configs/GameConfig";

        protected override void Awake()
        {
            base.Awake();
            LoadConfig();
        }

        private void LoadConfig()
        {
            TextAsset configFile = Resources.Load<TextAsset>(CONFIG_PATH);
            if (configFile == null)
            {
                Debug.LogError($"Failed to load config file at {CONFIG_PATH}");
                return;
            }

            string[] lines = configFile.text.Split('\n');
            if (lines.Length < 2) return;

            string[] headers = lines[0].Trim().Split(',');
            string[] values = lines[1].Trim().Split(',');

            for (int i = 0; i < headers.Length; i++)
            {
                if (i < values.Length && int.TryParse(values[i], out int value))
                {
                    configValues[headers[i]] = value;
                }
            }
        }

        public int GetValue(string key, int defaultValue = 0)
        {
            return configValues.TryGetValue(key, out int value) ? value : defaultValue;
        }

        public int GetCoinPoint()
        {
            return 50;
        }

        // Convenience methods for specific config values
        public int GetInitHealth()
        {
            return GetValue("InitHealth", 100);
        }

        public int GetAscendSpeed()
        {
            return GetValue("AscendSpeed", 5);
        }
    }
}