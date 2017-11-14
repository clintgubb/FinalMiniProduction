using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorTools
{
    [MenuItem("EMM/Clear Game Data", false)]
    public static void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Game Data Cleared!");
    }
}
