using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour {

    public float speed;

    public bool submerging;


    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject cube10;

    

	// Use this for initialization
	void Start () {
        speed = 10.0F;
        submerging = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(cube1.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube2.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube3.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube4.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube5.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube6.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube7.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube8.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube9.transform.position, transform.position) < 40.0F ||
            Vector3.Distance(cube10.transform.position, transform.position) < 40.0F)
        {
            submerging = true;
        }
        else
        {
            submerging = false;
        }

        if (submerging)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (!submerging)
        {
            if (transform.position.y < -6.5F)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}


   



}
