using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    float timer;
    float fY;
    float fX;
    float delay;

    Animator myAnimator;
    
	// Use this for initialization
	void Start () {
        delay = Random.Range(0, 150);
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Translate(Vector3.left * Time.deltaTime * 2);

        if (delay <= 0)
        {
            if (timer <= 0)
            {
                fX = Random.Range(-10.0f, 10.0f);
                fY = Random.Range(-4.0f, 4.0f);
                //Move waves
                gameObject.transform.position = new Vector3(fX, fY, 0.5f);
                myAnimator.Play(0);
                timer = Random.Range(0.8f, 3.0f);

                
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            delay--;
        }		
	}
}
