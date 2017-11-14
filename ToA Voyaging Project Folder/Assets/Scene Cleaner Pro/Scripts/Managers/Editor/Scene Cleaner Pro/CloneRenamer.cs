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
    using System.Text.RegularExpressions;
    public class CloneRenamer
    {
        static Regex _regexMainUnityClone = new Regex(@"[ ][(]\d*[)]\z");
        static Regex _regexMainCleanerClone = new Regex(@"[-]\d{3}\z");

        /// <summary>
        /// Initiates the clone renaming process 
        /// </summary>
        public static void Rename()
        {
            List<GameObject> objects = Resources.FindObjectsOfTypeAll<GameObject>().ToList();

            List<GameObject> temp = objects.ToList();

            foreach (GameObject gameObject in temp)
            {
                if ((_regexMainUnityClone.IsMatch(gameObject.name) || _regexMainCleanerClone.IsMatch(gameObject.name)) && objects.Contains(gameObject))
                {
                    objects = FindClones(gameObject, objects);
                }
            }

        }

        /// <summary>
        /// Finds all objects with the same name as obj
        /// </summary>
        /// <param name="obj">The object that is being renamed</param>
        /// <param name="objList">A list of all remaining objects in the scene</param>
        /// <returns></returns>
        private static List<GameObject> FindClones(GameObject obj, List<GameObject> objList)
        {
            string objBaseName;
            if (_regexMainCleanerClone.IsMatch(obj.name))
            {
                objBaseName = _regexMainCleanerClone.Replace(obj.name, "");
                Debug.Log("cleaner "+ objBaseName + "test");
            }
            else
            {
                objBaseName = _regexMainUnityClone.Replace(obj.name, "");
                Debug.Log("unity " + objBaseName + "test");

            }
            //Checks if the objcet is doubled numbered ie "foo-001 (2)" and prevents it from being so
            if (_regexMainCleanerClone.IsMatch(objBaseName))
            {
                objBaseName = objBaseName.Remove(objBaseName.Length - 4);
                Debug.Log(objBaseName);
            }

            string rgxUnityCloneString = @"\A("+ objBaseName +@" )[(]\d*[)]";
            string rgxCleanerCloneString = @"\A("+ objBaseName + @"-)\d{3}";


            Regex rgxUnityClone = new Regex(rgxUnityCloneString);

            Regex rgxCleanerClone = new Regex(rgxCleanerCloneString);
            List<GameObject> toRename = new List<GameObject>();
            List<GameObject> temp = objList.ToList();

            foreach (GameObject gameObject in temp)
            {
                if (rgxUnityClone.IsMatch(gameObject.name) || rgxCleanerClone.IsMatch(gameObject.name) ||gameObject.name == objBaseName)
                {

                    toRename.Add(gameObject);
                    objList.Remove(gameObject);
                }
            }
            
            

            NewNames(toRename, objBaseName);

            return objList;
        }

        /// <summary>
        /// Renames the objects in the objList
        /// </summary>
        /// <param name="objList">The list of objects to be renamed</param>
        /// <param name="objBaseName">Base name of the objects before numbering</param>
        private static void NewNames (List<GameObject> objList, string objBaseName)
        {

            objList = objList.OrderBy(o => o.name).ToList();
            for (int i = 0; i < objList.Count; i++)
            {
                if (i < 10)
                {
                    objList[i].name = objBaseName + "-00" + i.ToString();
                }
                else if (i < 100)
                {
                    objList[i].name = objBaseName + "-0" + i.ToString();
                }
                else if (i < 1000)
                {
                    objList[i].name = objBaseName + "-" + i.ToString();
                }
                else
                {
                    Debug.LogError("Over 1000 objects with one name, only first 1000 renamed");
                    break;
                }
            }

        }
    }
}
