using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.SceneCleanerPro.Editor
{
    public class ComponentPicker : EditorWindow
    {
        private Vector2 scrollPosition;
        private static ComponentPicker _window;
        private string search;

        public static ComponentPicker window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<ComponentPicker>(false, "Component Picker", false);

                return _window;
            }
        }


        public static void ShowWindow()
        {
            _window = GetWindow<ComponentPicker>(false, "Component Picker", true);
        }

        private void OnEnable()
        {
            search = "";
            minSize = new Vector2(400f, 400.0f);

        }
        public void OnGUI()
        {
            GUI.SetNextControlName("SearchBar");

            EditorGUI.FocusTextInControl("SearchBar");
            search = EditorGUILayout.TextField("Search component", search);
            Rule currentRule = CleanerManager.currentRuleset.currentRule;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            if (currentRule is RuleComponent)
            {
                RuleComponent currentComponentRule = (RuleComponent)currentRule;
                for (int i = 0; i < currentComponentRule.componentsNames.Length; i++)
                {
                    string tempToLowerName = currentComponentRule.componentsNames[i].ToLower();
                    string tempToLowerSearch = search.ToLower();
                    if (tempToLowerName.Contains(tempToLowerSearch))
                    {

                        if (GUILayout.Button(currentComponentRule.componentsNames[i]))
                        {

                            currentComponentRule.componentIndex = Rule.GetChosenStringIndex(currentComponentRule.componentsNames[i], currentComponentRule.componentsNames);
                            CleanerManager.currentRuleset.currentRule = currentComponentRule;
                            window.Close();
                        }
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
