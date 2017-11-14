using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Devdog.SceneCleanerPro.Editor
{
    public class CleanWindow : EditorWindow
    {

        private static CleanWindow _window;

        public static CleanWindow window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<CleanWindow>(false, "Cleaner manager", false);

                return _window;
            }
        }

        [MenuItem(ProductConstants.ToolsMenuPath + "Clean", false, -96)]
        public static void ShowWindow()
        {
            _window = GetWindow<CleanWindow>(false, "Clean window", true);
        }

        public void OnEnable()
        {
            minSize = new Vector2(50.0f, 50.0f);
        }

        void OnGUI()
        {
            if (GUILayout.Button("Clean", GUILayout.MaxWidth(200)))
            {
                CleanerManager.Clean();
            }
            if (GUILayout.Button("Reverse clean", GUILayout.MaxWidth(200)))
            {
                CleanerManager.ReverseClean();
            }

            if (CleanerOptionsWindow.LoadSettings().enableBetaFeatures)
            {
                if (GUILayout.Button("Rename numbered clones", GUILayout.MaxWidth(200)))
                {
                    CloneRenamer.Rename();
                }
                if (GUILayout.Button("Clean components", GUILayout.MaxWidth(200)))
                {
                    CleanerSelectionWindow.objectsToClean = DisabledCleanUp.CleanComponents();
                    CleanerSelectionWindow.checkComponents = true;
                    CleanerSelectionWindow.ShowWindow();
                }
                if (GUILayout.Button("Clean disabled objects", GUILayout.MaxWidth(200)))
                {
                    CleanerSelectionWindow.objectsToClean = DisabledCleanUp.CleanObjects();
                    CleanerSelectionWindow.checkComponents = false;
                    CleanerSelectionWindow.ShowWindow();
                }
                if (GUILayout.Button("Clean animations", GUILayout.MaxWidth(200)))
                {
                    AnimationCleanUp.ShowWindow();
                }
            }

        }
    }
}
