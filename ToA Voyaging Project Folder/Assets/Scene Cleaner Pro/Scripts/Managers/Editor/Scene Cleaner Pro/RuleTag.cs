using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace Devdog.SceneCleanerPro.Editor
{
    public class RuleTag : Rule
    {

        private string[] _tags;
        [SerializeField]
        string _tag;
        int _index;



        protected override void CleaningAction()
        {
            foreach (GameObject gameObject in objectsToClean)
            {            
                if (gameObject.tag == _tag)
                {
                    AddToGroup(gameObject);
                }
            }
        }

        protected override void DrawingAction()
        {
            _index = EditorGUILayout.Popup("Tag", _index, _tags);
            _tag = _tags[_index];
        }

        public override void OnLoad()
        {
            _tags = UnityEditorInternal.InternalEditorUtility.tags;
            _index = GetChosenStringIndex(_tag, _tags);
        }
    }
}
