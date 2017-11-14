using System;
using UnityEngine;
using UnityEditor;


namespace Devdog.SceneCleanerPro.Editor
{

    public class CleanerOptionsWindow : EditorWindow
    {
        private static CleanerSettings _settings;

        private static CleanerOptionsWindow _window;

        public static CleanerOptionsWindow window
        {
            get
            {
                
                if (_window == null)
                    _window = GetWindow<CleanerOptionsWindow>(false, "Cleaner options", false);

                return _window;
            }
        }

        [MenuItem(ProductConstants.ToolsMenuPath + "Cleaner options", false, -97)]
        public static void ShowWindow()
        {
            _window = GetWindow<CleanerOptionsWindow>(false, "Cleaner options", true);
        }

        private void OnEnable()
        {
            minSize = new Vector2(825.0f, 400.0f);

            _settings = LoadSettings();

        }

        /// <summary>
        /// Loads and returns the settings file for Scene Cleaner Pro
        /// </summary>
        /// <returns></returns>
        public static CleanerSettings LoadSettings()
        {
            CleanerSettings temp;
            try
            {
                string toDeserialize = StringSerializationAPI.JsonRead("Scene Cleaner Pro/SceneCleanerProSettings");
                temp = (CleanerSettings)StringSerializationAPI.Deserialize(typeof(CleanerSettings), toDeserialize);
            }
            catch (Exception)
            {
                temp = new CleanerSettings();
            }
            
            return temp;
            
        }

        private void OnDisable()
        {
            if (_settings.saveSettingsOnWindowClose)
            {
                SaveOptions();
            }
        }

        /// <summary>
        /// Draws the cleaner settings
        /// </summary>
        void OnGUI()
        {
            _settings.DrawOptions();
            if (GUILayout.Button("Save settings", GUILayout.MaxWidth(200)))
            {
                SaveOptions();
            }
        }

        /// <summary>
        /// Saves the settings file for Scene Cleaner Pro
        /// </summary>
        private static void SaveOptions()
        {
            string toSerialize = StringSerializationAPI.Serialize(typeof(CleanerSettings), _settings);
            StringSerializationAPI.JsonWrite("Scene Cleaner Pro/SceneCleanerProSettings", toSerialize);
            AssetDatabase.Refresh();
            
        }

        public static void SetCurrentOpen(Ruleset openRuleset)
        {
            _settings = LoadSettings();
            _settings.rulesetBeingEdited = openRuleset.rulesetName;
            SaveOptions();
        }
    }
}
