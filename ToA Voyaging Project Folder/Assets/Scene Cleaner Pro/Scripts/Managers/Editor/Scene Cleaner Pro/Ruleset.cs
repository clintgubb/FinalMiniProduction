using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


namespace Devdog.SceneCleanerPro.Editor
{
    using System;
    using System.Linq;

    public class Ruleset
    {
        private Color _colors;
        private Rule _currentRule;
        [SerializeField]
        private string _rulesetName;
        [SerializeField]
        private List<Rule> _rules;
        private Vector2 _scrollPosition;
        private int _ruleIndex;
        private Type[] newRule;
        private string[] newRuleName;

        public string rulesetName
        {
            get
            {
                return _rulesetName;
            }
            set
            {
                _rulesetName = value;
            }
        }

        public List<Rule> rules
        {
            get
            {
                return _rules;
            }
        }

        public Rule currentRule
        {
            get
            {
                return _currentRule;
            }

            set
            {
                _currentRule = value;
            }
        }

        public Ruleset()
        {
            if (EditorGUIUtility.isProSkin)
            {
                _colors = new Color(0.6f, 0.6f, 0.6f);
            }
            else
            {
                _colors = new Color(0.7f, 0.7f, 0.7f);
            }
            _rules = new List<Rule>();

            _rulesetName = "";

            newRule = ReflectionUtility.GetAllTypesThatImplement(typeof(Rule));
            newRuleName = CleanerManager.GetLastString(CleanerManager.TypeArrayToStringArray(newRule), '.');
        }
        

        /// <summary>
        /// Draws the entire ruleset in the manager
        /// </summary>
        public void DrawRuleset()
        {

            
            if (_rulesetName == "")
            {
                GUI.color = Color.red;
            }
            EditorGUIUtility.fieldWidth = 400;
            _rulesetName = EditorGUILayout.TextField("Ruleset name", _rulesetName);
            GUI.color = Color.white;
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.fieldWidth = 100;
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Width(250), GUILayout.MaxHeight(CleanerManager.window.position.height-50), GUILayout.MinHeight(CleanerManager.window.position.height - 60) };
            EditorGUILayout.BeginVertical(options);
            EditorGUILayout.LabelField("Rules");
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.MinHeight(CleanerManager.window.position.height - 85));

            List<Rule> ruleOverview = _rules.OrderBy(o => o.groupName).ToList();
            for (int i = 0; i < ruleOverview.Count(); i++)
            {
                string ruleGroupName = ruleOverview[i].groupName;
                if (!ruleOverview[i].isEnabled)
                {
                    GUI.color = _colors;
                }
                if (ruleGroupName == "")
                {
                    ruleGroupName = "Unnamed rule group";
                }
                
                if (GUILayout.Button(ruleGroupName + " : " +ruleOverview[i].priority, GUILayout.Width(225)))
                {
                    currentRule = ruleOverview[i];
                }
                GUI.color = Color.white;
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.BeginHorizontal();

            _ruleIndex = EditorGUILayout.Popup(_ruleIndex, newRuleName);
            if (GUILayout.Button("New Rule", GUILayout.MaxWidth(100)))
            {
                _rules.Add((Rule)Activator.CreateInstance(newRule[_ruleIndex]));
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUIUtility.labelWidth = 100;
            EditorGUIUtility.fieldWidth = 100;


            EditorGUILayout.BeginVertical(GUILayout.MinWidth(500));
            if (currentRule != null)
            {
                string[] tempName = new string[] { currentRule.GetType().ToString() };
                EditorGUILayout.LabelField(CleanerManager.GetLastString(tempName, '.')[0]);
                currentRule.DrawRule();
                if (currentRule.supRuleset == "Ruleset_" + _rulesetName)
                {
                    currentRule.supRuleset = "-";
                    Debug.LogWarning("Can't assign ruleset as supruleset in own rules");
                }
                if (GUILayout.Button("Delete", GUILayout.MaxWidth(100)))
                {
                    _rules.Remove(currentRule);
                    currentRule = null;
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            
        }

        /// <summary>
        /// Cleans the hierachy based on the ruleset
        /// </summary>
        /// <param name="ruleGroupName">Name of parent rule, only applicable if ruleset is a supruleset</param>
        public void CleanHierachy(string ruleGroupName = "")
        {
            List<Rule> toClean = _rules.OrderByDescending(o => o.priority).ToList();
            for (int i = 0; i < toClean.Count; i++)
            {
                if (toClean[i].isEnabled)
                {
                    toClean[i].CleanHierachy(ruleGroupName);
                }
            }
        }

        /// <summary>
        /// Cleans the hierachy based on the ruleset and cleaning area
        /// </summary>
        /// <param name="areaToClean">The are getting cleaned</param>
        public void CleanHierachy(CleaningArea areaToClean)
        {
            List<Rule> toClean = _rules.OrderByDescending(o => o.priority).ToList();
            for (int i = 0; i < toClean.Count; i++)
            {
                if (toClean[i].isEnabled)
                {
                    toClean[i].CleanHierachy(areaToClean);
                }
            }
        }


    }
}
