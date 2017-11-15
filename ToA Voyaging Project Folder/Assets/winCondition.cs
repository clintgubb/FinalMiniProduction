using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winCondition : MonoBehaviour {

    public GameObject boss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(boss.transform.position, transform.position)>200)
        {
            //trigger lose
            //fade to lose screen
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "boss")
        {
            //trigger win
            //fade to win screen
        }
    }
}
