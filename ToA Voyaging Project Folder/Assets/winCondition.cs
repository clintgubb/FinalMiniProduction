using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class winCondition : MonoBehaviour {

    public GameObject boss;
    public AnimationClip fadeColorAnimationClip;
    public Animator animColorFade;
    public GameObject canvas;

    Fader fader; 
    // Use this for initialization
    void Start () {
        fader = FindObjectOfType<Fader>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(boss.transform.position, transform.position)>300)
        {
            //trigger lose
            //fade to lose screen

            print("You lose!");

            canvas.SetActive(false);
            Invoke("LoadDelayedLose", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");


        }

        if (Vector3.Distance(boss.transform.position, transform.position) < 20)
        {
            //trigger lose
            //fade to lose screen

            print("You win!");

            canvas.SetActive(false);
            Invoke("LoadDelayedWin", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");


        }
    }

    public void LoadDelayedWin()
    {
        //Pause button now works if escape is pressed since we are no longer in Main menu.

        //Load the selected scene, by scene index number in build settings
        SceneManager.LoadScene(4);
    }

    public void LoadDelayedLose()
    {
        //Pause button now works if escape is pressed since we are no longer in Main menu.

        //Load the selected scene, by scene index number in build settings
        SceneManager.LoadScene(3);
    }


}
