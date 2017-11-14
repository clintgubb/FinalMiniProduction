using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWater : MonoBehaviour {

    // Use this for initialization

    public GameObject moveSeaOne;
    public GameObject moveSeaTwo;
    public GameObject moveSeaThree;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Sea_One_collider")
        {
            moveSeaOne.transform.position = new Vector3(0.0f, 0.0f, moveSeaOne.transform.position.z + 300);
            print("Sea One moved");
        }

        if (other.gameObject.name == "Sea_Two_collider")
        {
            moveSeaTwo.transform.position = new Vector3(0.0f, 0.0f, moveSeaTwo.transform.position.z + 300);
            print("Sea Two moved");
        }

        if (other.gameObject.name == "Sea_Three_collider")
        {
            moveSeaThree.transform.position = new Vector3(0.0f, 0.0f, moveSeaThree.transform.position.z + 300);
            print("Sea Three moved");
        }

    }
}
