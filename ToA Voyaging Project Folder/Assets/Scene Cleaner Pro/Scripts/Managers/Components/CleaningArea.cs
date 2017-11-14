using UnityEngine;
using System.Collections;

namespace Devdog.SceneCleanerPro.Editor
{
    using System.Collections.Generic;
    //using UnityEditor;
    [RequireComponent(typeof(BoxCollider))]
    public class CleaningArea : MonoBehaviour
    {

        [SerializeField]
        private string _ruleset;
        [SerializeField]
        private int _priority;

        public string ruleset
        {
            get
            {
                return _ruleset;
            }

            set
            {
                _ruleset = value;
            }
        }

        public int priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;
            }
        }

        /// <summary>
        ///Makes sure that the BoxCollider does not interfere with the scene when in playmode
        /// </summary>
        void Awake()
        {
            GetComponent<BoxCollider>().isTrigger = true;
            Destroy(GetComponent<CleaningArea>());
            Destroy(GetComponent<BoxCollider>());
        }

    }
}
