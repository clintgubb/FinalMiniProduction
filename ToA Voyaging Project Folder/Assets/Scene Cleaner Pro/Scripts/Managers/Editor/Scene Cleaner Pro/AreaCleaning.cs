using UnityEngine;
using System.Collections;

namespace Devdog.SceneCleanerPro.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    public class AreaCleaning
    {
        /// <summary>
        /// Finds all objects with cleaning area in the scene
        /// </summary>
        /// <returns></returns>
        public static List<GameObject> GetAllCleaningAreas()
        {
            List<GameObject> cleaningAreas = CleanerManager.rootObjectsInActiveScene.FindAll(x => x.GetComponent<CleaningArea>() != null);

            return cleaningAreas;
        }
        /// <summary>
        /// Finds all objects with a CleaningArea and returns them as a list
        /// </summary>
        /// <param name="toClean">The area to clean</param>
        /// <returns></returns>
        public static List<GameObject> ObjectsInAreaToClean(CleaningArea toClean)
        {
            List<GameObject> rootObjects = CleanerManager.rootObjectsInActiveScene.ToList();
            List<GameObject> cleanable = new List<GameObject>();
            foreach (GameObject gameObject in rootObjects)
            {
                if (toClean.GetComponent<BoxCollider>().bounds.Contains(gameObject.transform.position))
                {
                    cleanable.Add(gameObject);
                }
            }
            for (int i = 0; i < toClean.transform.childCount; i++)
            {
                if (!cleanable.Contains(toClean.transform.GetChild(i).gameObject))
                {
                    cleanable.Add(toClean.transform.GetChild(i).gameObject);
                }
            }
            return cleanable;
        }

        /// <summary>
        /// Cleans all CleaningAreas
        /// </summary>
        public static void CleanAreas()
        {
            List<GameObject> AreasToClean = GetAllCleaningAreas();
            AreasToClean = AreasToClean.OrderByDescending(o => o.GetComponent<CleaningArea>().priority).ToList();
            for (int i = 0; i < AreasToClean.Count; i++)
            {
                if (AreasToClean[i].GetComponent<CleaningArea>().enabled)
                {
                    CleaningArea temp = AreasToClean[i].GetComponent<CleaningArea>();
                    CleanSingleArea(temp);
                }
            }
        }

        /// <summary>
        /// Cleans a single CleaningArea
        /// </summary>
        /// <param name="toClean">The area to clean</param>
        private static void CleanSingleArea(CleaningArea toClean)
        {
            Ruleset ruleset; 

                
            ruleset = CleanerManager.LoadRuleset(toClean.GetComponent<CleaningArea>().ruleset);

            if (ruleset.rulesetName == "")
            {
                Debug.Log("No such ruleset in cleaning area " + toClean.name);
                return;
            }
            ruleset.CleanHierachy(toClean);

        }


    }
}
