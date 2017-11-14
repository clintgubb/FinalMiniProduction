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
    public class DisabledCleanUp
    {
        /// <summary>
        /// Gets a list all gameobject that have disabled components attached
        /// </summary>
        /// <returns></returns>
        public static List<GameObject> CleanComponents()
        {
            List<GameObject> allObjects = Resources.FindObjectsOfTypeAll<GameObject>().ToList();

            List<GameObject> objectsWithDisabledComponents = new List<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (HasDisabledComponents(obj) && obj.scene.name != null)
                {
                    objectsWithDisabledComponents.Add(obj);
                }
            }
            return objectsWithDisabledComponents;
            
        }

        /// <summary>
        /// Checks if a component has disabled compoenents attached
        /// </summary>
        /// <param name="obj">the gameobject to check on</param>
        /// <returns></returns>
        private static bool HasDisabledComponents(GameObject obj)
        {
            List<Component> compoenents = obj.GetComponents<Component>().ToList();
            foreach (Component comp in compoenents)
            {
                if (comp is Behaviour)
                {
                    if (!(comp as Behaviour).enabled)
                    {
                        return true;
                    }
                }
                else if (comp is Renderer)
                {
                    if (!(comp as Renderer).enabled)
                    {
                        return true;
                    }
                }
                else if (comp is Collider)
                {
                    if (!(comp as Collider).enabled)
                    {
                        return true;
                    }
                }

                
            }
            return false;
        }

        /// <summary>
        /// Gets a list of all gameobjects that are disabled in the scene
        /// </summary>
        /// <returns></returns>
        public static List<GameObject> CleanObjects()
        {
            List<GameObject> allObjects = Resources.FindObjectsOfTypeAll<GameObject>().ToList();

            List<GameObject> disabledObjects = new List<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (!obj.activeInHierarchy && obj.scene.name != null)
                {
                    disabledObjects.Add(obj);
                }
            }

            return disabledObjects;
        }

    }
}
