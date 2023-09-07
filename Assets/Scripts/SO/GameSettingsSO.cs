using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace OLS_HyperCasual
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SO/GameSettingsSO", order = 51)]
    [Serializable]
    public class GameSettingsSO : ScriptableObject
    {
        [SerializeField] private List<GameSettingsTreeElement> settings;
    
#if UNITY_EDITOR
        public List<GameSettingsTreeElement> Settings
        {
            get => settings;
            set => settings = value;
        }
#endif

        public int GetIntValue(string key, bool showExistError = true, int defaultValue = 0)
        {
            var value = GetValue(key);
            if (string.IsNullOrEmpty(value))
            {
                if (showExistError)
                {
                    Debug.LogError($"[GameSettingsSO.GetIntValue]: Cannot find key `{key}`");
                }
            
                return defaultValue;
            }

            if (int.TryParse(value, out var intValue))
            {
                return intValue;
            }
    
            Debug.LogError($"[GameSettingsSO.GetIntValue]: Cannot Parse key `{key}` with value `{value}`");
            return defaultValue;
        }

        public float GetFloatValue(string key, bool showExistError = true, float defaultValue = 0)
        {
            var value = GetValue(key);
            if (string.IsNullOrEmpty(value))
            {
                if (showExistError)
                {
                    Debug.LogError($"[GameSettingsSO.GetFloatValue]: Cannot find key `{key}`");
                }
                
                return defaultValue;
            }

            if (float.TryParse(value, NumberStyles.Number, CultureInfo.GetCultureInfo("ru-RU"),  out var floatValue))
            {
                return floatValue;
            }

            Debug.LogError($"[GameSettingsSO.GetFloatValue]: Cannot Parse key `{key}` with value `{value}`");
            return defaultValue;
        }

        public string GetValue(string key)
        {
            foreach (var data in settings)
            {
                if (data.key == key)
                {
                    return data.value;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct SettingKVData
    {
#if UNITY_EDITOR
        public GameObject targetGameObject;
#endif
        public string key;
        public string value;
    }
}