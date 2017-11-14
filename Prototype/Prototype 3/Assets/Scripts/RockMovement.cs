using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    int position;
	// Use this for initialization
	void Start ()
    {
        position = Random.Range(1, 3);

        if (position == 1)
        {
            gameObject.transform.position = new Vector3(15.0f, 3.0f, -1.0f);
        }
        else if (position == 2)
        {
            gameObject.transform.position = new Vector3(15.0f, -0.4f, -1.0f);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * 2);

        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
}
