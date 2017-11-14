using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour {

    [Header("Player")]
    [Tooltip("Automatically finds the Player with tag 'Player'")]
    public Transform player;

    [Space(10)]
    [Header("Load Variables")]
    [Tooltip("From which slot the game has been loaded")]
    public int loadedSlotId;
    [Tooltip("From which save trigger the game has been saved")]
    public int saveTriggerId;
    [Tooltip("Array of all save triggers present")]
    public SaveGameTrigger[] allTriggers;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //retrieve which slot is loaded
        loadedSlotId = PlayerPrefs.GetInt("slotLoaded_");

        //search all save triggers
        allTriggersPresent();

        //now see if the game is loaded from a saved game
        hasLoadedGame();

        //now hide UI
        GetComponent<UIController>().hideMenus();
    }

    /// <summary>
    /// Search for all the trigger present in Scene
    /// </summary>
    void allTriggersPresent() {
        allTriggers = FindObjectsOfType<SaveGameTrigger>();
    }

    /// <summary>
    /// check if the player has loaded the game from menu
    /// </summary>
    void hasLoadedGame() {
        //check if it's loaded from a slot
        if (loadedSlotId != 0) {
            //find the trigger which was used to save this slot
            
            //1. retrieve save trigger's id from saved data
            saveTriggerId = PlayerPrefs.GetInt("saveTriggerId_" + loadedSlotId);

            //2. match it with all the available save triggers
            for(int i = 0; i<allTriggers.Length; i++)
            {

                //3. once the match is found
                if(allTriggers[i].saveTriggerId == saveTriggerId)
                {
                    //4. spawn player at that trigger's spawn point location
                    spawnPlayerAtPoint(allTriggers[i].spawnPoint);
                    saveName = allTriggers[i].saveName + loadedSlotId;
                }
            }
        }
    }

    public void spawnPlayerAtPoint(Transform spawnPoint) {
        player.position = spawnPoint.position;
        
    }

    string saveName;
    public string retSaveName()
    {
        return saveName;
    }
    
}
