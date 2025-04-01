using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityCommunity.UnitySingleton;

namespace _Custom
{
    public class ConfigManager : MonoSingleton<ConfigManager>
    {
        private Dictionary<string, float> configValues = new();
        //Config Path: GameConfig.csv
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

        public float GetValue(string key, float defaultValue = 0)
        {
            return configValues.TryGetValue(key, out float value) ? value : defaultValue;
        }

        public int GetCoinPoint()
        {
            return 50;
        }

        // Convenience methods for specific config values
        public int GetInitHealth()
        {
            var data = GetValue("InitHealth", 3);
            return (int)data;
        }
        
        public float GetFlyAcceleration()
        {
            return GetValue("AscendSpeed", 0.5f);

        }

        public float GetMaxFlySpeed()
        {
            return GetValue("MaxFlySpeed", 10);
        }

        public float GetDescendSpeed()
        {
            return GetValue("DescentSpeed", 8);
        }
    }
}