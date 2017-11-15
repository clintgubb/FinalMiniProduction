using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{

    // Use this for initialization
    Animator myAnimator;
    float timer;
    bool tutorial;
    float count;
    public GameObject nowText;
    public GameObject pressText;
    public GameObject frame;


    void Start()
    {
        myAnimator = GetComponent<Animator>();
        timer = 1.70F;
        tutorial = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial)
        {
            if (timer <= 0)
            {
                //now
                nowText.SetActive(true);
                frame.SetActive(true);
            }

            if (count > 6)
            {
                tutorial = false;
                // nowText.SetActive(false);
                pressText.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowText.SetActive(false);
                frame.SetActive(false);
                count++;
            }
        }



        if (Input.GetKey(KeyCode.Space))
        {
            if (timer <= 0)
            {
                myAnimator.Play(0);
                timer = 1.70F;
            }          
        }
        timer -= Time.deltaTime;
    }
}
