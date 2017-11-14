using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace Devdog.SceneCleanerPro.Editor
{
    public class RuleNamePrefix : Rule
    {


        [SerializeField]
        string _prefix;



        protected override void CleaningAction()
        {
            foreach (GameObject gameObject in objectsToClean)
            {
                if (gameObject.name.StartsWith(_prefix))
                {
                    AddToGroup(gameObject);
                }
            }
        }


        protected override void DrawingAction()
        {
            _prefix = EditorGUILayout.TextField("Name prefix", _prefix);
        }




    }
}
