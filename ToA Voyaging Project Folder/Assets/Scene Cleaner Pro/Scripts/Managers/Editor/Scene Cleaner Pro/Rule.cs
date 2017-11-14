using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Devdog.SceneCleanerPro.Editor
{
    using System.Linq;
    public abstract class Rule
    {
        [SerializeField]
        private int _priority;
        [SerializeField]
        private string _groupName;
        [SerializeField]
        private bool _isEnabled;
        [SerializeField]
        private string _supRuleset;

        private List<GameObject> _objectsToClean;
        private GameObject _groupObject;

        public string groupName
        {
            get
            {
                return _groupName;
            }            
        }

        public bool isEnabled
        {
            get
            {
                return _isEnabled;
            }
        }

        public string supRuleset
        {
            get
            {
                return _supRuleset;
            }
            set
            {
                _supRuleset = value;
            }
        }

        public int priority
        {
            get
            {
                return _priority;
            }
        }

        protected List<GameObject> objectsToClean
        {
            get
            {
                return _objectsToClean;
            }
        }

        protected GameObject groupObject
        {
            get
            {
                return _groupObject;
            }
        }

        public Rule()
        {
            _isEnabled = true;
            _groupName = "";
            _supRuleset = "-";
            OnLoad();
        }
        
        /// <summary>
        /// Draws the options for the rule
        /// </summary>
        public void DrawRule()
        {
            if (_groupName == "")
            {
                GUI.color = Color.red;
            }
            _groupName = EditorGUILayout.TextField("Group name", _groupName);
            GUI.color = Color.white;

            EditorGUIUtility.fieldWidth = 20;
            _priority = EditorGUILayout.IntField("Rule priority", _priority);
            EditorGUIUtility.fieldWidth = 100;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set supruleset"))
            {
                _supRuleset = CleanerManager.GetRulesetsInSaveFolder();
                if (_supRuleset == "")
                {
                    _supRuleset = "-";
                }
                
            }
            if (GUILayout.Button("Create supruleset"))
            {
                Ruleset newSupruleset = new Ruleset();
                newSupruleset.rulesetName = groupName + "_Supruleset";
                _supRuleset = "Ruleset_" + newSupruleset.rulesetName;
                CleanerManager.SaveRuleset(newSupruleset);


            }
            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = 1;
            EditorGUILayout.LabelField(_supRuleset);
            EditorGUIUtility.labelWidth = 100;
            if (GUILayout.Button("Clear supruelset"))
            {
                _supRuleset = "-";
            }
            _isEnabled = EditorGUILayout.Toggle("Is enabled", _isEnabled);


            DrawingAction();



        }

        /// <summary>
        /// Cleans the hierachy based on the rule
        /// </summary>
        /// <param name="ruleGroupName">Name of parent rule, only applicable if rule is in supruleset</param>
        public void CleanHierachy(string ruleGroupName)
        {
            _objectsToClean = new List<GameObject>();
            if (ruleGroupName == "")
            {
                _objectsToClean = CleanerManager.rootObjectsInActiveScene.ToList();
                AssignGroupObject(true);
            }
            else
            {
                GameObject objectGroup = GameObject.Find(ruleGroupName);
                int objectNumber = objectGroup.transform.childCount;
                for (int i = 0; i < objectNumber; i++)
                {
                    objectsToClean.Add(objectGroup.transform.GetChild(i).gameObject);
                }
                AssignGroupObject(false);
            }
            _objectsToClean = RemoveExclusionObjects(_objectsToClean);
            CleaningAction();

            if (supRuleset != "-")
            {
                CleanerManager.LoadRuleset(supRuleset).CleanHierachy(groupName);
            }
        }
        /// <summary>
        /// Cleans the hierachy based on the rule and cleaning area
        /// </summary>
        /// <param name="areaToClean">The are getting cleaned</param>
        public void CleanHierachy(CleaningArea areaToClean)
        {
            _objectsToClean = new List<GameObject>();


            _objectsToClean = AreaCleaning.ObjectsInAreaToClean(areaToClean);

            

            try
            {
                _groupObject = areaToClean.transform.Find("Group_" + groupName).gameObject;
            }
            catch (System.Exception)
            {

                _groupObject = new GameObject("Group_" + groupName);
                _groupObject.transform.position = areaToClean.transform.position;
                _groupObject.transform.parent = areaToClean.transform;
            }

            _objectsToClean = RemoveExclusionObjects(_objectsToClean);
            CleaningAction();

            if (supRuleset != "-")
            {
                CleanerManager.LoadRuleset(supRuleset).CleanHierachy(groupName);
            }
        }

        /// <summary>
        /// Removes objects that contain a cleaning exclusion from the list getting cleaned
        /// </summary>
        /// <param name="objectsToCheck">List of objects that are getting cleaned</param>
        /// <returns></returns>
        public static List<GameObject> RemoveExclusionObjects(List<GameObject> objectsToCheck)
        {
            List<GameObject> temp = objectsToCheck.ToList();
            foreach (GameObject gameObject in temp)
            {
                CleaningExclusion exclusion = gameObject.GetComponent<CleaningExclusion>();
                if (exclusion != null && exclusion.enabled)
                {
                    objectsToCheck.Remove(gameObject);
                }
            }
            return objectsToCheck;
        }


        /// <summary>
        /// Assigns the object that all objects that match the cleaning parameter are placed under
        /// </summary>
        /// <param name="isRoot">Is the objects getting cleaned root objects? (have no parents)</param>
        protected void AssignGroupObject(bool isRoot)
        {
            _groupObject = _objectsToClean.Find(x => x.name == "Group_" + groupName);
            if (_groupObject == null)
            {
                _groupObject = new GameObject("Group_" + groupName);
                
                if (!isRoot)
                {
                    groupObject.transform.parent = objectsToClean[0].transform.parent;
                }
                objectsToClean.Remove(groupObject);
                
            }
        }

        /// <summary>
        /// Puts a GameObjects under the Group object
        /// </summary>
        /// <param name="toAdd">Object to add</param>
        protected void AddToGroup(GameObject toAdd)
        {
            toAdd.transform.parent = groupObject.transform;
        }

        /// <summary>
        /// The action preformed by the specific rule to clean the hierachy
        /// </summary>
        protected abstract void CleaningAction();
        /// <summary>
        /// The unique drawin action of the specific rule
        /// </summary>
        protected abstract void DrawingAction();


        /// <summary>
        /// Is called whenever the rule is loaded, use instead of constructer when making new rules
        /// </summary>
        public virtual void OnLoad()
        {

        }


        /// <summary>
        /// Helper method for making new kinds of rules, given an object and an array of objects it will return the index that the object is at in the array, if not in the array returns 0
        /// </summary>
        /// <param name="chosen">Object to look for</param>
        /// <param name="allObjects">Array to look in</param>
        /// <returns></returns>
        public static int GetChosenObjectIndex(object chosen, object[] allObjects)
        {
            for (int i = 0; i < allObjects.Length; i++)
            {
                if (chosen == allObjects[i])
                {
                    return i;
                }
            }
            return 0;
        }


        /// <summary>
        /// Helper method for making new kinds of rules, given string and an array of strings it will return the index that the string is at in the array, if not in the array returns 0
        /// </summary>
        /// <param name="chosen">String to look for</param>
        /// <param name="allObjects">Array to look in</param>
        /// <returns></returns>
        public static int GetChosenStringIndex(string chosen, string[] allObjects)
        {
            for (int i = 0; i < allObjects.Length; i++)
            {
                if (chosen == allObjects[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
