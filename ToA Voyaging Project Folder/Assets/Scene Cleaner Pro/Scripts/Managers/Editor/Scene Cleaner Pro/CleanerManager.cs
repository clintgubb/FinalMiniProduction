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
    public class CleanerManager : EditorWindow
    {
            
        private static List<GameObject> _rootObjectsInActiveScene;

        private static Ruleset _currentRuleset;

        private int _loadRulesetAtIndex;

        private static CleanerManager _window;

        public static CleanerManager window
        {
            get
            {
                if (_window == null)
                    _window = GetWindow<CleanerManager>(false, "Cleaner manager", false);

                return _window;
            }
        }

        public static Ruleset currentRuleset
        {
            get
            {
                return _currentRuleset;
            }

            set
            {
                _currentRuleset = value;
            }
        }

        public static List<GameObject> rootObjectsInActiveScene
        {
            get
            {
                List<GameObject> temp = Resources.FindObjectsOfTypeAll<GameObject>().ToList();

                List<GameObject> rootObjectsInScene = new List<GameObject>();


                foreach (GameObject obj in temp)
                {
                    if (obj.scene == SceneManager.GetActiveScene() && obj.transform.parent == null)
                    {
                        rootObjectsInScene.Add(obj);
                    }
                }

                return rootObjectsInScene;
            }
        }


        [MenuItem(ProductConstants.ToolsMenuPath + "Cleaner manager", false, -99)]
        public static void ShowWindow()
        {
            _window = GetWindow<CleanerManager>(false, "Unity Cleaner", true);
        }
        
        private void OnEnable()
        {
            minSize = new Vector2(825.0f, 400.0f);
            currentRuleset = LoadRuleset("Ruleset_" + CleanerOptionsWindow.LoadSettings().rulesetBeingEdited);
            if (currentRuleset == null)
            {

                currentRuleset = new Ruleset();
            }
            

        }

        private void OnDisable()
        {
            if (CleanerOptionsWindow.LoadSettings().saveRulesetOnWindowclose)
            {
                SaveRuleset(currentRuleset);
            }
            CleanerOptionsWindow.SetCurrentOpen(currentRuleset);
        }

        /// <summary>
        /// Starts the cleaning of the hierachy
        /// </summary>
        public static void Clean()
        {
            ReverseClean();
            AreaCleaning.CleanAreas();
            Ruleset toClean;
            if (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode3D)
            {
                toClean = LoadRuleset("Ruleset_MainRuleset3D");

            }
            else
            {
                toClean = LoadRuleset("Ruleset_MainRuleset2D");
            }
            toClean.CleanHierachy();
        }

        /// <summary>
        /// Draws the manager
        /// </summary>
        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
            if (GUILayout.Button("New ruleset"))
            {
                currentRuleset = new Ruleset();
            }

            if (GUILayout.Button("Save ruleset"))
            {
                SaveRuleset(currentRuleset);
            }

            if (GUILayout.Button("Load ruleset"))
            {
                string toLoad = GetRulesetsInSaveFolder();
                currentRuleset = LoadRuleset(toLoad);
            }

            EditorGUILayout.EndHorizontal();


            if (currentRuleset != null)
            {
                currentRuleset.DrawRuleset();
            }

            Repaint();
        }

        /// <summary>
        /// Saves any given ruleset in the set save folder
        /// </summary>
        /// <param name="toSave">Ruleset to save</param>
        public static void SaveRuleset(Ruleset toSave)
        {
            if (toSave.rulesetName != "" && toSave.rulesetName != "-")
            {
                string toSerialize = StringSerializationAPI.Serialize(typeof(Ruleset),toSave);
                StringSerializationAPI.JsonWrite(GetSavePath() + "Ruleset_" + toSave.rulesetName, toSerialize);
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Loads a rule by name from the set save folder
        /// </summary>
        /// <param name="name">Name of ruleset to load</param>
        /// <returns></returns>
        public static Ruleset LoadRuleset(string name)
        {
            Ruleset temp;
            try
            {
                string toDeserialize = StringSerializationAPI.JsonRead(GetSavePath() + name);
                temp = (Ruleset)StringSerializationAPI.Deserialize(typeof(Ruleset), toDeserialize);
            }
            catch (Exception)
            {
                if (currentRuleset == null)
                {
                    temp = new Ruleset();
                }
                else
                {
                    temp = currentRuleset;
                }
            }
            foreach (Rule rule in temp.rules)
            {
                
                rule.OnLoad();
            }

            return temp;
        }

        /// <summary>
        /// Gets the path to the save folder
        /// </summary>
        /// <returns></returns>
        private static string GetSavePath()
        {
            
            CleanerSettings temp = CleanerOptionsWindow.LoadSettings();
            return temp.savePath;
        }

        /// <summary>
        /// Given an array of Type, it returns an array of string that are the names of the types
        /// </summary>
        /// <param name="toConvert">Type array to get names from</param>
        /// <returns></returns>
        public static string[] TypeArrayToStringArray(Type[] toConvert)
        {
            string[] arrayToReturn = new string[toConvert.Length];
            for (int j = 0; j < toConvert.Length; j++)
            {
                arrayToReturn[j] = toConvert[j].ToString();
            }
            return arrayToReturn;
        }

        /// <summary>
        /// Returns an array of strings that have been split and now are only the last element of the spilt
        /// </summary>
        /// <param name="toSplit">Array to spilt srings of</param>
        /// <param name="splitChar">Char the strings are split by</param>
        /// <returns></returns>
        public static string[] GetLastString(string[] toSplit, char splitChar)
        {
            for (int i = 0; i < toSplit.Length; i++)
            {
                toSplit[i] = toSplit[i].Split(splitChar)[toSplit[i].Split(splitChar).Length - 1];
            }
            return toSplit;
        }

        /// <summary>
        /// Returns a string array with the names of all rulesets
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllRulesets()
        {
            UnityEngine.Object[] allRulesets = AssetDatabase.LoadAllAssetsAtPath("Assets/" + GetSavePath());
            string[] toReturn = new string[allRulesets.Length];
            for (int i = 0; i < allRulesets.Length; i++)
            {
                toReturn[i] = allRulesets[i].name;
            }
            return toReturn;
        }

        /// <summary>
        /// Returns the name of a ruleset in a folder
        /// </summary>
        /// <returns></returns>
        public static string GetRulesetsInSaveFolder()
        {
            string toReturn = EditorUtility.OpenFilePanel("Load ruleset", "Assets/" + GetSavePath(), "");
            toReturn = toReturn.Split('/')[toReturn.Split('/').Length - 1];
            toReturn = toReturn.Split('.')[0];
            return toReturn;
        }

        public static void ReverseClean()
        {
            Ruleset mainRuleset;
            if (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode3D)
            {
                mainRuleset = LoadRuleset("Ruleset_MainRuleset3D");

            }
            else
            {
                mainRuleset = LoadRuleset("Ruleset_MainRuleset2D");
            }
            RemoveChildrenOfRule(mainRuleset);

            List<GameObject> areasToReverseClean = AreaCleaning.GetAllCleaningAreas();

            foreach (GameObject area in areasToReverseClean)
            {
                Ruleset tempRuleset = LoadRuleset(area.GetComponent<CleaningArea>().ruleset);
                area.transform.DetachChildren();
                RemoveChildrenOfRule(tempRuleset);
            }
        }


        /// <summary>
        /// Reverse cleans the hierarchy so that everything is being cleaned properly in case rulesets change and an object should be in another group
        /// </summary>
        /// <param name="tempRuleset">The ruleset to reverse clean</param>
        private static void RemoveChildrenOfRule(Ruleset tempRuleset)
        {
            foreach (Rule rule in tempRuleset.rules)
            {
                GameObject tempObject = rootObjectsInActiveScene.Find(x => x.name == "Group_" + rule.groupName);
                if (tempObject != null)
                {
                    tempObject.transform.DetachChildren();
                    DestroyImmediate(tempObject);
                    if (rule.supRuleset != "" && rule.supRuleset != "-")
                    {
                        Ruleset supRuleset = LoadRuleset(rule.supRuleset);
                        RemoveChildrenOfRule(supRuleset);
                    }

                }

            }
        }
    }
}