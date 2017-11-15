using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rhythmKeeper : MonoBehaviour {

    float timer;
    public Sprite mySpr;
    public GameObject test;
	// Use this for initialization
	void Start () {
        timer = 0F;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0)
        {
            Instantiate<GameObject>(test);
            timer = 1.70F;
        }
        else
        {
            timer -= Time.deltaTime;
        }
	}
}
