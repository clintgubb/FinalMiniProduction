using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {

    [Tooltip("Do you want Level Select Menu?")]
    public bool hasLevelSelection = true;

    [Header("Level Select Properties")]
    [Tooltip("Root Level Select Menu")]
    public GameObject levelSelectMenu;
    [Tooltip("Level Select Items will be here!")]
    public Transform levelItemsContainer;
    [Tooltip("Level Item Prefab")]
    public LevelSelectItem LevelItemPrefab;
    [Space(10)]

    [Header("Level Item Configuration")]
    [Tooltip("Define everything here!")]
    public List<LevelItemsConfiguration> levelItemsConfiguration;
   
    // Use this for initialization
    void Start () {

        //if we want level selection screen
        if (hasLevelSelection)
        {
            InitializeLevelSelectMenu();
        }
        //destroy everything!
        else {
            Destroy(levelSelectMenu);
            Destroy(this);
        }
	}

    public void openLevelSelect()
    {

        GetComponent<Animator>().Play("LevelSelect_on");

        //play sfx
        EasyAudioUtility.instance.Play("Click");
    }

    public void closeLevelSelect()
    {
        GetComponent<Animator>().Play("LevelSelect_off");
    }

    void InitializeLevelSelectMenu()
    {
        for (int i = 0; i < levelItemsConfiguration.Count; i++)
        {
            //1 .Instantiate level Item
            LevelSelectItem item = createLevelItem();

            //2. Initialize Item
            item.setLevelItem(levelItemsConfiguration[i].levelImage, i+1,
                levelItemsConfiguration[i].levelName, levelItemsConfiguration[i].hasSubLevel,
                levelItemsConfiguration[i].subLevelName, 
                levelItemsConfiguration[i].levelToLoad, levelItemsConfiguration[i].isLocked);
        }
    }

    LevelSelectItem createLevelItem() {
        //Instantiate level Item
        LevelSelectItem item = Instantiate(LevelItemPrefab);
        item.transform.SetParent(levelItemsContainer);
        item.transform.localScale = Vector3.one;
        item.transform.localPosition = Vector3.zero;

        return item;
    }

}

[System.Serializable]
public class LevelItemsConfiguration {
    public string levelName;
    public bool hasSubLevel;
    public string subLevelName;
    public bool isLocked;
    public string levelToLoad;
    public Sprite levelImage;
}
