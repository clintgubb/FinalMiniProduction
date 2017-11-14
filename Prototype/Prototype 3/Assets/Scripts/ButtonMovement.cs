using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour {

 

	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(15.0f, 6.3f, -1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.Translate(Vector3.left * Time.deltaTime * 4);

        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }

    }
}
