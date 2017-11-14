using UnityEngine;
using UnityEditor;
using System;

namespace Devdog.SceneCleanerPro.Editor
{
    public class CleanerSettings
    {
        [SerializeField]
        private string _savePath;
        private string[] _rulesets;
        [SerializeField]
        private bool _saveRulesetOnWindowclose, _saveSettingsOnWindowClose;
        [SerializeField]
        private string _rulesetBeingEdited;
        [SerializeField]
        bool _enableBetaFeatures;
        

        public string savePath
        {
            get
            {
                return _savePath;
            }
        }

        public bool saveRulesetOnWindowclose
        {
            get
            {
                return _saveRulesetOnWindowclose;
            }
        }

        public bool saveSettingsOnWindowClose
        {
            get
            {
                return _saveSettingsOnWindowClose;
            }
        }

        public string rulesetBeingEdited
        {
            get
            {
                return _rulesetBeingEdited;
            }

            set
            {
                _rulesetBeingEdited = value;
            }
        }

        public bool enableBetaFeatures
        {
            get
            {
                return _enableBetaFeatures;
            }
        }

        public CleanerSettings()
        {
            _savePath = "";
            _saveSettingsOnWindowClose = true;
            _saveRulesetOnWindowclose = true;
        }


        /// <summary>
        /// Draws the Cleaner settings
        /// </summary>
        public void DrawOptions()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Choose save path", GUILayout.MaxWidth(200)))
            {
                _savePath = EditorUtility.OpenFolderPanel("Save path", "Assets", "");
                _savePath = GetReletivPath(_savePath);

            }
            
            _savePath = EditorGUILayout.TextField(_savePath);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.HelpBox("Choose path to save and load rulesets from, cannot choose a path outside the Assets folder, no path means the Assets folder", MessageType.None);

            EditorGUIUtility.labelWidth = 200;
            _saveRulesetOnWindowclose = EditorGUILayout.Toggle("Save ruleset on window close", _saveRulesetOnWindowclose);
            _saveSettingsOnWindowClose = EditorGUILayout.Toggle("Save settings on window close", _saveSettingsOnWindowClose);;

            _enableBetaFeatures = EditorGUILayout.Toggle("Enable beta features", _enableBetaFeatures);
        }

        
        /// <summary>
        /// Returns a path relative to the Assets folder from a full path
        /// </summary>
        /// <param name="fullPath">Path to find relative path of</param>
        /// <returns></returns>
        private string GetReletivPath(string fullPath)
        {
            string[] temp = fullPath.Split('/');
            string toReturn = "";
            bool valildPath = false;

            for (int i = temp.Length - 1; i > -1; i--)
            {
                if (temp[i] == "Assets")
                {
                    valildPath = true;
                    break;
                }
                toReturn = temp[i] + "/" + toReturn;
            }

            if (valildPath)
            {
                return toReturn;
            }
            else
            {
                return "";
            }
        }


    }
}
