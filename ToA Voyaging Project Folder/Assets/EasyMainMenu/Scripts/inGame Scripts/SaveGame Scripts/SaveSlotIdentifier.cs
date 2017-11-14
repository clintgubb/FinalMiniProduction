using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotIdentifier : MonoBehaviour {

    [Header("Slot specific Variables")]
    [Tooltip("if true, this Slot will become the quick save slot!")]
    public bool quickSaveSlot;
    [Tooltip("Unique Slot ID used to identify while loading")]
    public int slotId;
    [Tooltip("This Slot's name field")]
    public Text saveName_text;
    [Tooltip("This Slot's percentage field")]
    public Text savePercentage_text;
    [Tooltip("Parent UI Class to send slot specific data")]
    public SaveGameUI saveGameUI;
    [Tooltip("Unique save trigger id ")]
    public int saveTriggerId;
	
    // Use this for initialization
	void Start () {
        loadSlotData();
	}

    public void sendSlotData() {

        saveGameUI.saveName_text = saveName_text;
        saveGameUI.savePercentage_text = savePercentage_text;
        saveGameUI.slotId = slotId;

    }

    void loadSlotData() {

       
        //if there's already data present on this slot
        if (PlayerPrefs.GetInt("slot_" + slotId) == slotId) {
            //then load it and set it at UI
            saveName_text.text = PlayerPrefs.GetString("slot_saveName_" + slotId);
            savePercentage_text.text = PlayerPrefs.GetFloat("slot_savePercentage_" + slotId) + "%";

        }

        //get key
        quickSaveSlot = PlayerPrefs.GetInt("quickSaveSlot") == slotId ? true : false;
        //if true
        if (quickSaveSlot)
        {
            //change color
            Color c = Color.red;
            c.a = 0.25f;
            GetComponent<Image>().color = c;
            //disable btn
            GetComponentInChildren<Button>().enabled = false;
            //remove percentage from here
            savePercentage_text.text = "";

            //now finally sending the data
            saveGameUI.quickSaveSlotData(saveName_text,savePercentage_text, slotId);

        }
    }

    
}
