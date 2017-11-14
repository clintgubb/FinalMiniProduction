using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    [Tooltip("Scene retrieved to load. If empty, it will load main menu.")]
    public string sceneToLoad;

    [Header("Loading Bar")]
    [Tooltip("Loading Bar")]
    public Slider loadingBar;
    [Tooltip("Show loading bar or circular loading indicator.")]
    public bool showLoadingBar;
    [Tooltip("Loading Bar fill delay.")]
    public float fillDelay = 0.5f;
    [Tooltip("Loading Bar fill speed.")]
    public float fillSpeed = 0.5f;

    [Header("Circular Indicator")]
    [Tooltip("Circular loading delay.")]
    public GameObject circularIndicator;
    [Tooltip("Scene Load Delay.")]
    public float circularLoadDelay = 6f;
    [Tooltip("Circular Indicator rotation speed.")]
    public float circularIndicatorAnimSpeed = 1f;

    [Header("Loading Screen Image Transition")]
    [Tooltip("Loading Screen image")]
    public Image defaultLoadingScreenImage;
    [Tooltip("If it's true, images will show one after another, else any random image will be shown from below array.")]
    public bool showImageTransition = true;
    [Tooltip("Add 1280x720 res images if it's landscape menu")]
    public Sprite[] LoadingScreenImages;
    [Tooltip("How long an image will be displayed")]
    [Range(3f,10f)]
    public float transitionDuration;
    [Tooltip("Transition Fader")]
    public Animator transitionFader;
    // Use this for initialization
    void Start()
    {
        //init loading bar one time and make scene ready to load
        init();

        if (showLoadingBar)
        //using coroutine for performance
        InvokeRepeating("fillLoadingBar", fillDelay, fillSpeed);
       
    }

    void init() {
        //retrieve what scene to be loaded
        sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
        //if it's null
        if (sceneToLoad == "")
        {
            sceneToLoad = "MainMenu";
        }

        //now loading scene async
        //SceneManager.LoadSceneAsync(sceneToLoad);
        SceneManager.LoadSceneAsync(sceneToLoad).allowSceneActivation = false;

        //load on the basis of loading bar fill amount
        if (showLoadingBar)
        {
            //init loading bar
            loadingBar.minValue = 0f;
            loadingBar.maxValue = 100f;
            loadingBar.value = 0f;

            //enable right object
            loadingBar.gameObject.SetActive(true);
            circularIndicator.SetActive(false);

        }
        //else load after circular laod delay
        else
        {
          //  enable right object
            loadingBar.gameObject.SetActive(false);
            circularIndicator.SetActive(true);

            //set anim speed
            circularIndicator.GetComponent<Animator>().speed = circularIndicatorAnimSpeed;
            Invoke("loadScene", circularLoadDelay);
        }

        //enable loading screen transitions
        if (showImageTransition)
        {
            defaultLoadingScreenImage.color = Color.white;
            //now invoking transitions
            InvokeRepeating("StartImageTransition", 0f, transitionDuration);

        }
        //else set a random image from array to screen image
        else
        {
            //if any image is added
            if(LoadingScreenImages.Length > 0)
            {
                defaultLoadingScreenImage.color = Color.white;
                defaultLoadingScreenImage.sprite = LoadingScreenImages[Random.Range(0, LoadingScreenImages.Length)];
            }
        }
       
    }

    void fillLoadingBar() {
        //increase it's value by 1,2 or 3
        loadingBar.value += Random.Range(0,10);

        if (loadingBar.value == 100) {
            Debug.Log("load scene");
            loadScene();
            CancelInvoke("fillLoadingBar");
        }
    }

    
    int i = 0;
    /// <summary>
    /// Invoking TransitionFader with the interval of -0.5f
    /// </summary>
    void StartImageTransition()
    {
        if (i < LoadingScreenImages.Length)
        {
            defaultLoadingScreenImage.sprite = LoadingScreenImages[i];
            CancelInvoke("TransitionFader");
            Invoke("TransitionFader", transitionDuration - 0.5f);
            i++;
        }else
        {
            i = 0;
            defaultLoadingScreenImage.sprite = LoadingScreenImages[i];
            CancelInvoke("TransitionFader");
            Invoke("TransitionFader", transitionDuration - 0.5f);
            i++;
        }
    }

    void TransitionFader()
    {
       // Debug.Log("TransitionFader");
        transitionFader.Play("Transition");
    }


    void loadScene() {
        //begin fader
        Animator Fader = GameObject.Find("Fader").GetComponent<Animator>();
        Fader.GetComponent<Animator>().Play("Fader In");
        Invoke("load", 1f);
    }

    void load() {
        //delete key
        PlayerPrefs.DeleteKey("sceneToLoad");
        //finally load scene
        SceneManager.LoadScene(sceneToLoad);

    }
}
