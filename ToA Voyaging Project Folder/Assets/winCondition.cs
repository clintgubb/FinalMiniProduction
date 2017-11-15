using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class winCondition : MonoBehaviour {

    public GameObject boss;

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
            SceneManager.LoadScene("LoseScreen");
            //fader.FadeIntoLevel("LoseScreen");


        }

        if (Vector3.Distance(boss.transform.position, transform.position) < 12)
        {
            //trigger lose
            //fade to lose screen


            print("You win!");

            SceneManager.LoadScene("WinScreen");


        }
    }


}
