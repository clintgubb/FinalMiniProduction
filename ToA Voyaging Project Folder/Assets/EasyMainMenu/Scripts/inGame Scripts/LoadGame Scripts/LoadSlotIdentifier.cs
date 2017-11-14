using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSlotIdentifier : MonoBehaviour {

    [Header("Slot specific Variables")]
    [Tooltip("if true, this Slot will become the quick save slot!")]
    public bool quickSaveSlot;
    [Tooltip("Unique Slot ID used to identify while loading")]
    public int slotId;
    [Tooltip("This Slot's name field")]
    public Text saveName_text;
    [Tooltip("This Slot's percentage field")]
    public Text savePercentage_text;
    [Tooltip("Scene to load")]
    public string sceneToLoad;

    // Use this for initialization
    void Awake () {

        Init();

        //if this is the gameplay scene, send data
        UIController controller = FindObjectOfType<UIController>();
        if (controller != null)
        {
            controller.loadSlots.Add(this);
        }
    }

    //retrieve saved data
    public void Init() {
        //first load slot data
        loadSlotData();

       
        
    }

    void loadSlotData()
    {
        //get key
        quickSaveSlot = PlayerPrefs.GetInt("quickSaveSlot") == slotId ? true : false;
        //if it's quick save make it different
        //change color
        if (quickSaveSlot)
        {
            //change color
            Color c = Color.red;
            c.a = 0.25f;
            GetComponent<Image>().color = c;
        }

        //if there's already data present on this slot
        if (PlayerPrefs.GetInt("slot_" + slotId) == slotId)
        {
            //then load it and set it at UI
            saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + slotId);
            sceneToLoad = PlayerPrefs.GetString("slot_sceneName_" + slotId);

            if (!quickSaveSlot)
                savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + slotId) + "%";
            else
                savePercentage_text.text = "";

        }

    }

    public void LoadSceneSaved()
    {
        //if there's a save game present at this slot
        if (sceneToLoad != "")
        {
            Time.timeScale = 1;

            //delete player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player)
                Destroy(player);

            //save which slot is loaded
            PlayerPrefs.SetInt("slotLoaded_", slotId);

            //loads a specific scene
            PlayerPrefs.SetString("sceneToLoad", sceneToLoad);

            //load level via fader
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeIntoLevel("LoadingScreen");

            
        }
    }
}
