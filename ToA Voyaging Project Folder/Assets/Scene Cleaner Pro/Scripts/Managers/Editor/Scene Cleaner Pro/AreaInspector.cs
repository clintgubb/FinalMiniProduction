using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Devdog.SceneCleanerPro.Editor
{
    [CustomEditor(typeof(CleaningArea))]
    public class AreaInspector : UnityEditor.Editor
    {

        /// <summary>
        /// Draws a special inspector for cleaning areas
        /// </summary>
        public override void OnInspectorGUI()
        {


            CleaningArea tar = (CleaningArea)target;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set area ruleset", GUILayout.MaxWidth(150)))
            {
                tar.ruleset = CleanerManager.GetRulesetsInSaveFolder();
            }
            EditorGUILayout.LabelField("Current ruleset: " + tar.ruleset);
            EditorGUILayout.EndHorizontal();

            tar.priority = EditorGUILayout.IntField("Priority", tar.priority);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

