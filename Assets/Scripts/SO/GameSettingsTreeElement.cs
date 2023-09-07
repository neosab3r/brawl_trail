using System;
using UnityEditor.TreeViewExamples;
using UnityEngine;
using Random = System.Random;

namespace OLS_HyperCasual
{
    [Serializable]
    public class GameSettingsTreeElement : TreeElement
    {
        public string key;
        public string value;
        public ESettingElementType settingElementType;
        public bool enabled;

        public GameSettingsTreeElement (ESettingElementType settingElementType, string key, int depth, int id) : base (key, depth, id)
        {
            this.settingElementType = settingElementType;
            this.key = key;
            enabled = true;
        }

        public void SetValue(string value)
        {
            this.value = value;
        }

        public void SetKey(string key)
        {
            this.key = key;
            this.name = key;
        }

    }
}