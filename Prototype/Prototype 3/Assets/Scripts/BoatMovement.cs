using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatMovement : MonoBehaviour {

    public bool canMove;
    float move;
    bool button;

	// Use this for initialization
	void Start () {

        canMove = false;

        move = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (!canMove)
        {
            if (transform.position.x < -6)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 2);
            }
            else
            {
                canMove = true;
            }
        }

        if (canMove)
        {

            //Controller movement input
            move = Input.GetAxis("Horizontal");
            if (move > 0)
            {
                //Moving right
                //transform.Translate(Vector3.right * Time.deltaTime);
            }
            else if (move < 0)
            {
                //moving back
                //transform.Translate(Vector3.left * Time.deltaTime * 4);
            }

            move = Input.GetAxis("Vertical");
            if (move > 0 && transform.position.y < 3.36)
            {
                //Moving up
                transform.Translate(Vector3.up * Time.deltaTime * 2);
            }
            else if (move < 0 && transform.position.y > -1.00)
            {
                //Moving down
                transform.Translate(Vector3.down * Time.deltaTime * 2);
            }

            //Controller SHAPE button input
            //if (button = Input.GetButtonDown("Square"))
            //{
            //    print("Square working");
            //}
            //if (button = Input.GetButtonDown("X"))
            //{
            //    print("X working");
            //}
            //if (button = Input.GetButtonDown("Circle"))
            //{
            //    print("O working");
            //}
            //if (button = Input.GetButtonDown("Triangle"))
            //{
            //    print("Triangle working");
            //}
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rock")
        {
            SceneManager.LoadScene("Gameover");
        }
    }

}
