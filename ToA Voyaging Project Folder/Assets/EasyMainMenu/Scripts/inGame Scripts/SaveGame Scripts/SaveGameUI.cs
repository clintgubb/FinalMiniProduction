using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameUI : MonoBehaviour {

    [Header("Quick Save Key")]
    public KeyCode key;

    [Header("Save Trigger Data")]
    [Tooltip("No Need to Assign")]
    public string saveName;
    [Tooltip("No Need to Assign")]
    public float savePercentage;

    [Header("Save Slots UI")]
    [Tooltip("No Need to Assign")]
    public Text saveName_text;
    [Tooltip("No Need to Assign")]
    public Text savePercentage_text;

    [Header("Slot Specific Data ")]
    [Tooltip("ASSIGN ID TO THIS SLOT")]
    public int slotId;
    [Tooltip("No Need to Assign")]
    public int saveTriggerId;
    [Tooltip("No Need to Assign")]
    public string sceneName;

    [HideInInspector]
    public Canvas[] allUI;

    #region Quick Save Vars
    public Text QsaveName_text;
    public Text QsavePercentage_text;
    public int QslotId;
  
    #endregion

    // Use this for initialization
    IEnumerator Start () {

        //if (Camera.main.GetComponent<AudioListener>())
        //    GetComponent<AudioListener>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        loadQuickSaveData();
    }

    void Update()
    {
        if (Input.GetKeyDown(key)) {
            //Save Game
            SaveData_quickSlot();
            //Debug.Log("Save Game");
        }
    }



    #region Public Methods

    public void quickSaveSlotData(Text saveName, Text savePercentage, int slotID)
    {
        QsaveName_text = saveName;
        QsavePercentage_text = savePercentage;
        QslotId = slotID;

    }

    public void openConfirmation() {
        GetComponent<Animator>().Play("confirmUI_open");

        //play sound
        playClickSound();
    }

    //if yes, then save to current slot
    public void confirmation_yes()
    {
        saveData();

        //play anim
        GetComponent<Animator>().Play("confirmUI_close");

        //play sound
        playClickSound();
    }

    //get's back to save slot selection
    public void confirmation_no()
    {
        GetComponent<Animator>().Play("confirmUI_close");

        //play sound
        playClickSound();
    }

    public void closeSaveUI() {

        //disable all UI
        for (int i = 0; i < allUI.Length; i++)
        {
            allUI[i].gameObject.SetActive(true);
        }
        //un-pause game
        Time.timeScale = 1;
       
        //play UI anim
        GetComponent<Animator>().Play("saveGameUI_close");
        Invoke("disableUI", 0.2f);

        //play sfx
        playClickSound();
    }

    void disableUI() {
        GetComponent<UIController>().hideMenus();
    }
    #endregion

    void saveData() {
        //set data in UI
        saveName_text.text = saveName;
        savePercentage_text.text = savePercentage + "%";
        //transform as well

        //saving playerPrefs
        PlayerPrefs.SetInt("slot_" + slotId, slotId);
        PlayerPrefs.SetString("slot_saveName_" + slotId, saveName);
        PlayerPrefs.SetFloat("slot_savePercentage_" + slotId, savePercentage);
        PlayerPrefs.SetInt("saveTriggerId_" + slotId, saveTriggerId);
        PlayerPrefs.SetString("slot_sceneName_" + slotId, sceneName);

    }

    void SaveData_quickSlot()
    {
        //set data in UI
        QsaveName_text.text = "Quicksave : " + SceneManager.GetActiveScene().name;
        QsavePercentage_text.text = "";

        //saving playerPrefs
        PlayerPrefs.SetInt("QuickSaveDataIsPresent", 1);
        PlayerPrefs.SetInt("slot_" + QslotId, QslotId);
        PlayerPrefs.SetString("slot_saveName_" + QslotId, QsaveName_text.text);
        PlayerPrefs.SetString("slot_sceneName_" + QslotId, SceneManager.GetActiveScene().name);
        SavePositions();

        //play HUD animation
        Animator SaveText = GameObject.Find("SaveText").GetComponent<Animator>();
        if (!SaveText.IsInTransition(0)) {
            SaveText.Play("SaveTextHUD");
        }

    }

    /// <summary>
    /// Save Exact Positions of the player transform
    /// </summary>
    void SavePositions() {

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float x = player.position.x;
        float y = player.position.y;
        float z = player.position.z;

        PlayerPrefs.SetFloat("player.position.x", x);
        PlayerPrefs.SetFloat("player.position.y", y);
        PlayerPrefs.SetFloat("player.position.z", z);

    }

    /// <summary>
    /// Load the position
    /// AND
    /// Set the position
    /// </summary>
    void LoadnSetPosition() {

        float x = PlayerPrefs.GetFloat("player.position.x");
        float y = PlayerPrefs.GetFloat("player.position.y");
        float z = PlayerPrefs.GetFloat("player.position.z");

        Vector3 loadedPosition = new Vector3(x, y, z);

        //now find player!
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = loadedPosition;

    }

    /// <summary>
    /// We will load the quick save data if it's present 
    /// </summary>
    void loadQuickSaveData()
    {
        //if there's data present in the QuickSaveSlot
        if (PlayerPrefs.GetInt("slotLoaded_") == QslotId && QslotId != 0)
        {
            //Load Positions
            LoadnSetPosition();
        }
    }

    #region Sounds
    public void playHoverClip()
    {
        EasyAudioUtility.instance.Play("Hover");
       
    }

   public void playClickSound()
    {
        EasyAudioUtility.instance.Play("Click");
    }

    #endregion

}
