using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


namespace Devdog.SceneCleanerPro.Editor
{
    using System.Linq;
    using System.Reflection;
    public class AreaManager : EditorWindow
    {
        private Vector2 _scrollPosition;
        private Color[] _colors;
        private CleaningArea[] _areas;

        private static AreaManager _window;

        public static AreaManager window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<AreaManager>(false, "Cleaner manager", false);

                return _window;
            }
        }

        [MenuItem(ProductConstants.ToolsMenuPath + "Area manager", false, -98)]
        public static void ShowWindow()
        {
            _window = GetWindow<AreaManager>(false, "Area manager", true);
        }

        private void OnEnable()
        {
            if (EditorGUIUtility.isProSkin)
            {
                _colors = new Color[] { new Color(0.1f, 0.1f, 0.1f), new Color(0.2f, 0.2f, 0.2f) };
            }
            else
            {
                _colors = new Color[] { new Color(0.9f, 0.9f, 0.9f), new Color(0.8f, 0.8f, 0.8f) };
            }
            minSize = new Vector2(825.0f, 400.0f);
            _areas = GetAllAreas();


        }


        /// <summary>
        /// Gets all cleaning areas in the scene
        /// </summary>
        /// <returns></returns>
        private CleaningArea[] GetAllAreas()
        {
            CleaningArea[] allAreas = FindObjectsOfType<CleaningArea>();
            return allAreas;
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            try
            {
                for (int i = 0; i < _areas.Length; i++)
                {
                    Rect colorArea = EditorGUILayout.BeginVertical();
                    GUI.color = _colors[i % 2];
                    GUI.DrawTexture(colorArea, EditorGUIUtility.whiteTexture);
                    GUI.color = Color.white;
                    EditorGUILayout.LabelField("Area: " + _areas[i].name);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Choose area ruleset" ,GUILayout.MaxWidth(200)))
                    {
                        _areas[i].ruleset = CleanerManager.GetRulesetsInSaveFolder();
                    }
                    EditorGUILayout.HelpBox("Area ruleset: " + _areas[i].ruleset, MessageType.None);
                    EditorGUILayout.EndHorizontal();
                    _areas[i].priority = EditorGUILayout.IntField("Area priority", _areas[i].priority);
                    EditorGUILayout.EndVertical();

                }

            }
            catch (Exception)
            {

                _areas = GetAllAreas();
            }
            if (_areas.Length == 0)
            {
                EditorGUILayout.HelpBox("No areas in scene", MessageType.Warning);
            }
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Update area list"))
            {
                _areas = GetAllAreas();
            }
        }
    }
}
