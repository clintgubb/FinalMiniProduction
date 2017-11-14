using UnityEngine;
using UnityEditor;

namespace Devdog.SceneCleanerPro.Editor
{
    using System;
    public class RuleComponent : Rule
    {
        private int _componentIndex;
        [SerializeField]
        private Type _cleanComponent;
        private Type[] _components;
        private string[] _componentsNames;

        public string[] componentsNames
        {
            get
            {
                return _componentsNames;
            }
        }

        public int componentIndex
        {
            get
            {
                return _componentIndex;
            }

            set
            {
                _componentIndex = value;
            }
        }

        protected override void CleaningAction()
        {
            foreach (GameObject gameObject in objectsToClean)
            {
                if (gameObject.GetComponent(_cleanComponent) != null)
                {
                    AddToGroup(gameObject);
                }
            }
        }

        protected override void DrawingAction()
        {

            if (GUILayout.Button("Choose component"))
            {
                ComponentPicker.ShowWindow();
            }
            EditorGUILayout.LabelField("Current component: " + _componentsNames[_componentIndex]);
            _cleanComponent = _components[componentIndex];
        }

        public override void OnLoad()
        {
            _components = ReflectionUtility.GetAllTypesThatImplement(typeof(Component));
            _componentsNames = CleanerManager.GetLastString(CleanerManager.TypeArrayToStringArray(_components), '.');
            componentIndex = GetChosenObjectIndex(_cleanComponent, _components);
        }
    }
}
