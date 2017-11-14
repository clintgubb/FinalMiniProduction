using UnityEngine;
using UnityEditor;

namespace Devdog.SceneCleanerPro.Editor
{
    using System.Linq;
    public class RuleEmptyObjects : Rule
    {
        [SerializeField]
        private bool _deleteObjects;

        protected override void CleaningAction()
        {
            foreach (GameObject gameObject in objectsToClean)
            {
                if (gameObject != null && !HasChildren(gameObject) && IsEmpty(gameObject))
                {
                    
                    AddToGroup(gameObject);
                    
                }
            }
            if (_deleteObjects)
            {
                GameObject.DestroyImmediate(groupObject);
            }
        }

        /// <summary>
        /// Checks if the GameObject has children
        /// </summary>
        /// <param name="gameObject">GameObject to check</param>
        /// <returns></returns>
        private bool HasChildren(GameObject gameObject)
        {
            if (gameObject.transform.childCount == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Checks if the GameObject contains only one component i.e. the transform
        /// </summary>
        /// <param name="gameObject">GameObject to check</param>
        /// <returns></returns>
        private bool IsEmpty(GameObject gameObject)
        {
            if (gameObject.GetComponents<Component>().Count() == 1)
            {
                return true;
            }
            return false;
        }



        protected override void DrawingAction()
        {

            _deleteObjects = EditorGUILayout.Toggle("Delete objects", _deleteObjects);
        }
    }
}
