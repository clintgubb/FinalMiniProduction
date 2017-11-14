using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


namespace Devdog.SceneCleanerPro.Editor
{
    using System;
    using System.Linq;

    public class AnimationCleanUp : EditorWindow
    {
        private static AnimationCleanUp _window;
        private static List<GameObject> _objectsToClean;
        private Vector2 _scrollPosition;
        private GameObject _selectedObject;
        public static AnimationCleanUp window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<AnimationCleanUp>(false, "Animation Cleanup", false);

                return _window;
            }
        }


        public static void ShowWindow()
        {
            _window = GetWindow<AnimationCleanUp>(false, "Animation Cleanup", true);
            _objectsToClean = GetAllUncontrolledAnimators();
        }

        public void OnEnable()
        {
            minSize = new Vector2(200.0f, 200.0f);
        }

        void OnGUI()
        {
            if (_objectsToClean != null && _objectsToClean.Count > 0)
            {
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.MinHeight(0));
                foreach (GameObject obj in _objectsToClean)
                {
                    if (GUILayout.Button(obj.name, GUILayout.MaxWidth(200)))
                    {
                        _selectedObject = obj;
                        Selection.activeGameObject = _selectedObject;
                    }
                }                
                EditorGUILayout.EndScrollView();
                if (_selectedObject != null)
                {
                    if (GUILayout.Button("Ignore object", GUILayout.MaxWidth(200)))
                    {
                        _objectsToClean.Remove(_selectedObject);
                    }
                }

                if (GUILayout.Button("Destroy animators", GUILayout.MaxWidth(200)))
                {
                    if (EditorUtility.DisplayDialog("Destroy animators without contollers?", "Destroy animators without contollers? This is irreversible", "Yes", "No"))
                    {
                        DestroyAnimators();
                        _window.Close();
                    }
                }

            }
            else
            {
                EditorGUI.LabelField(new Rect(_window.position.width / 2 - 100, _window.position.height / 2 - 30, 200, 30), "No Animators need cleaning");
            }

        }

        /// <summary>
        /// Gets all gameobjects in the scene with animators without controllers
        /// </summary>
        /// <returns></returns>
        static List<GameObject> GetAllUncontrolledAnimators()
        {
            List<GameObject> allObjects = Resources.FindObjectsOfTypeAll<GameObject>().ToList();


            List<GameObject> objectsWithMissingAniController = new List<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Animator ani = obj.GetComponent<Animator>();
                if (ani != null)
                {
                    if (ani.runtimeAnimatorController == null)
                    {
                        objectsWithMissingAniController.Add(obj);
                    }
                }
            }
            return objectsWithMissingAniController;
        }

        /// <summary>
        /// Destroys all animators without controllers unless it has been ignored
        /// </summary>
        void DestroyAnimators()
        {
            foreach (GameObject obj in _objectsToClean)
            {
                if (obj.GetComponent<Animator>().runtimeAnimatorController == null)
                {
                    GameObject.DestroyImmediate(obj.GetComponent<Animator>());
                }
            }
            
        }


    }
}
