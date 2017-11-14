using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject boat;

    public bool canMove;

    float fX;
    public RockFrequency Distance;

    // Use this for initialization
    void Start()
    {
        canMove = false;        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Distance.SpawnDistance);
        if (transform.position.z <= boat.transform.position.z)
        {
            canMove = true;
        }
       // print(transform.position.z - boat.transform.position.z);

        if (canMove && Vector3.Distance(transform.position, boat.transform.position) > 100 )
        {
            fX = Random.Range(-50.0f, 50.0f);
            transform.position = new Vector3(fX, 0, boat.transform.position.z + Distance.SpawnDistance);

            canMove = false;
            Distance.SpawnDistance -= 10;
        }
    }
}
