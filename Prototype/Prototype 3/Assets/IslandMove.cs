using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMove : MonoBehaviour {

    public GameObject[] screen;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x >= 0)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 2);
        }

        if (transform.position.x >= 0 && transform.position.x <= 0.2)
        {
            Instantiate<GameObject>(screen[0]);
        }
    }
}
