using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    public GameObject boat;

    public bool canMove;

    float fX;

    // Use this for initialization
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z <= boat.transform.position.z + 5 && transform.position.z >= boat.transform.position.z - 5)
        {
            canMove = true;
        }
        print(transform.position.z - boat.transform.position.z);

        if (canMove && (transform.position.z - boat.transform.position.z > 100 || transform.position.z - boat.transform.position.z < -100))
        {
            fX = Random.Range(-50.0f, 50.0f);
            transform.position = new Vector3(fX, 0, boat.transform.position.z + 300);
            canMove = false;
        }
    }
}
