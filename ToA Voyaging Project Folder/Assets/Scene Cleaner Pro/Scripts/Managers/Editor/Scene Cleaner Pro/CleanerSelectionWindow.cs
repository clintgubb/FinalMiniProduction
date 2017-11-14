using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;


namespace Devdog.SceneCleanerPro.Editor
{
    using System.Linq;
    using System.Reflection;
    public class CleanerSelectionWindow : EditorWindow
    {
        private static CleanerSelectionWindow _window;
        private Vector2 _scrollPosition, _scrollPositionComponent;
        private static List<GameObject> _objectsToClean;
        private static bool _checkComponents;
        private List<Component> _componentsOnObj;
        private GameObject _selectedObject;
        public static CleanerSelectionWindow window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<CleanerSelectionWindow>(false, "Cleaner selector", false);

                return _window;
            }
        }

        public static List<GameObject> objectsToClean
        {
            get
            {
                return _objectsToClean;
            }

            set
            {
                _objectsToClean = value;
            }
        }

        public static bool checkComponents
        {
            get
            {
                return _checkComponents;
            }

            set
            {
                _checkComponents = value;
            }
        }

        public static void ShowWindow()
        {
            _window = GetWindow<CleanerSelectionWindow>(false, "Cleaner selector", true);
        }

        public void OnEnable()
        {
            minSize = new Vector2(825.0f, 400.0f);
        }

        void OnGUI()
        {
            if (_objectsToClean != null && _objectsToClean.Count > 0)
            {

                EditorGUILayout.BeginHorizontal();
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
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();

                if (!_checkComponents && _selectedObject != null)
                {
                    if (_selectedObject.activeInHierarchy)
                    {
                        if (GUILayout.Button("Deactivate object", GUILayout.MaxWidth(200)))
                        {
                            _selectedObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Activate object", GUILayout.MaxWidth(200)))
                        {
                            _selectedObject.SetActive(true);
                        }

                    }
                }
                if (_selectedObject != null)
                {
                    if (GUILayout.Button("Ignore object", GUILayout.MaxWidth(200)))
                    {
                        _objectsToClean.Remove(_selectedObject);
                    }
                }
                EditorGUILayout.EndHorizontal();
            
                _scrollPositionComponent = EditorGUILayout.BeginScrollView(_scrollPositionComponent, GUILayout.MinHeight(0));
                if (_selectedObject != null)
                {
                    _componentsOnObj = ComponentsOnObject(_selectedObject);
                    for (int i = 0; i < _componentsOnObj.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        string[] tempName = new string[] { _componentsOnObj[i].GetType().ToString() };
                        EditorGUILayout.LabelField(CleanerManager.GetLastString(tempName, '.')[0]);
                        if (_checkComponents && ComponentCanEnable(_componentsOnObj[i]))
                        {
                            if (ComponentEnabled(_componentsOnObj[i]))
                            {
                                if (GUILayout.Button("Deactivate", GUILayout.MaxWidth(200)))
                                {
                                    SetComponentActive(_componentsOnObj[i], false);
                                }
                            }
                            else
                            {
                                if (GUILayout.Button("Activate", GUILayout.MaxWidth(200)))
                                {
                                    SetComponentActive(_componentsOnObj[i], true);
                                }

                            }
                        }
                        EditorGUILayout.EndHorizontal();

                    }

                }
                else
                {
                    GUILayout.Label("No object selected");
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndScrollView();
                EditorGUILayout.EndHorizontal();
                if (_checkComponents)
                {
                    if (GUILayout.Button("Destroy disabled components", GUILayout.MaxWidth(200)))
                    {
                        if (EditorUtility.DisplayDialog("Destroy disabled components?", "Destroy disabled components? This is irreversible", "Yes", "No"))                        
                        {
                            DestroyComponents();
                            _window.Close();
                        }
                    }
                }
                else
                {
                    if (GUILayout.Button("Destroy disabled objects", GUILayout.MaxWidth(200)))
                    {
                        if (EditorUtility.DisplayDialog("Destroy disabled objects?", "Destroy disabled objects? This is irreversible", "Yes", "No"))
                        {
                            DestroyObjects();
                            _window.Close();
                        }
                    }
                }
            }
            else
            {
                EditorGUI.LabelField(new Rect(_window.position.width/2 - 100, _window.position.height/2 - 30, 200, 30), "No objects need cleaning");
            }
        }

        /// <summary>
        /// Returns a list of all components on the object
        /// </summary>
        /// <param name="gameObject">The object to get components from</param>
        /// <returns></returns>
        private List<Component> ComponentsOnObject(GameObject gameObject)
        {
            List<Component> components = new List<Component>();

            components = gameObject.GetComponents<Component>().ToList();

            return components;
        }

        /// <summary>
        /// Checks of a component is enabled
        /// </summary>
        /// <param name="comp">The compenent to chack</param>
        /// <returns></returns>
        private bool ComponentEnabled(Component comp)
        {
            
            if (comp is Behaviour)
            {
                if (!(comp as Behaviour).enabled)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (comp is Renderer)
            {
                if (!(comp as Renderer).enabled)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (comp is Collider)
            {
                if (!(comp as Collider).enabled)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }            
            return true;
        }
        /// <summary>
        /// Sets a component to be either active or inactive
        /// </summary>
        /// <param name="comp">The component to change</param>
        /// <param name="active">Set objects enable status</param>
        private void SetComponentActive(Component comp, bool active)
        {

            if (comp is Behaviour)
            {
                (comp as Behaviour).enabled = active;

            }
            else if (comp is Renderer)
            {
                (comp as Renderer).enabled = active;
            }
            else if (comp is Collider)
            {
                (comp as Collider).enabled = active;
            }
        }

        /// <summary>
        /// Checks if the componenet can be enabled
        /// </summary>
        /// <param name="comp">The componenet to check</param>
        /// <returns></returns>
        private bool ComponentCanEnable(Component comp)
        {
          
            if (comp is Behaviour)
            {
                
                return true;
                
            }
            else if (comp is Renderer)
            {
                
                return true;
                
            }
            else if (comp is Collider)
            {
                
                return true;
                
            }            
            return false;
        }

        /// <summary>
        /// Destroys all objects that are still not ignored or active
        /// </summary>
        void DestroyObjects()
        {
            foreach (GameObject obj in _objectsToClean)
            {
                if (!obj.activeInHierarchy)
                {
                    GameObject.DestroyImmediate(obj);    
                }
            }
        }

        /// <summary>
        /// Destroys all components that are not ignored or active
        /// </summary>
        void DestroyComponents()
        {
            foreach (GameObject obj in _objectsToClean)
            {
                List<Component> components = ComponentsOnObject(obj).ToList();
                foreach (Component comp in components)
                {
                    if (!ComponentEnabled(comp))
                    {
                        GameObject.DestroyImmediate(comp);
                    }
                }
            }
        }

    }
}
