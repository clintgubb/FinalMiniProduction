using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {

    // Use this for initialization
    Animator myAnimator;
    float timer;


	void Start () {
        myAnimator = GetComponent<Animator>();
        timer = 1.70F;
    }
	
	// Update is called once per frame
	void Update () {

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
