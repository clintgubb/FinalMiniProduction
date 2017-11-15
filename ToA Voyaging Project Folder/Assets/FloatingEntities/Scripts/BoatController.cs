using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour
{
    public PropellerBoats ship;
    bool forward = true;

    float spaceLimit = 1.70F;

    bool canMove = false;

    void Update()
    {
        //print(spaceLimit);
        //print(canMove);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (spaceLimit < 0 )
            {
                canMove = true;                          
            }
            else
            {
                ship.ThrottleDown();
                ship.Brake();
                canMove = false;
            }
            spaceLimit = 1.70F;
        }
        else if (spaceLimit < -2)
        {
            canMove = false;
            ship.ThrottleDown();
            ship.Brake();
        }


        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.A))
                ship.RudderLeft();
            if (Input.GetKey(KeyCode.D))
                ship.RudderRight();

            if (forward)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    ship.ThrottleUp();
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    ship.ThrottleDown();
                    ship.Brake();
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.S))
                    ship.ThrottleUp();
                else if (Input.GetKey(KeyCode.W))
                {
                    ship.ThrottleDown();
                    ship.Brake();
                }
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                ship.ThrottleDown();

            if (ship.engine_rpm == 0 && Input.GetKeyDown(KeyCode.S) && forward)
            {
                forward = false;
                ship.Reverse();
            }
            else if (ship.engine_rpm == 0 && Input.GetKeyDown(KeyCode.W) && !forward)
            {
                forward = true;
                ship.Reverse();
            }

        }
        spaceLimit -= Time.deltaTime;
    }

}
