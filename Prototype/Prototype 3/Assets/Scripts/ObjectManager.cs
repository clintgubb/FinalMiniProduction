using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public GameObject face;
    public Sprite faceSprite1;
    public Sprite faceSprite2;
    public Sprite faceSprite3;
    public Sprite boatSprite1;
    public Sprite boatSprite2;

    public GameObject Boat;

    public GameObject[] buttons;
    public GameObject[] rocks;

    List<GameObject> objects = new List<GameObject>();

    float timer;
    float buttonSpeed;
    float boatTimer;
    float nextRock;
    int index;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        boatTimer = 1;
        index = 0;
        nextRock = -1;
        buttonSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            index = Random.Range(0, 4);
            objects.Add(Instantiate(buttons[index]));
            timer = 1.0515f - buttonSpeed;

            if (buttonSpeed < 0.6)
            {
                buttonSpeed += 0.005f;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (buttonSpeed >= 0.59)
        {
            SceneManager.LoadScene("Win");
        }

        nextRock -= Time.deltaTime;

        if (boatTimer > 0.0f)
        {
            boatTimer -= Time.deltaTime;
        }
        else if(boatTimer < 0.0f && boatTimer > -2.0f)
        {
            Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;
            face.GetComponent<SpriteRenderer>().sprite = faceSprite3;
            boatTimer = -5.0f;
        }
        
        if (objects[0].transform.position.x < -8)
        {
            objects.RemoveAt(0);
            face.GetComponent<SpriteRenderer>().sprite = faceSprite2;
            Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;

            if (nextRock < 0.0f)
            {
                index = Random.Range(0, 2);
                Instantiate<GameObject>(rocks[index]);
                nextRock = 4.0f;
            }
        }

        if (Input.GetButtonDown("Square"))
        {
            if (objects[0].name == "Square(Clone)" && objects[0].transform.position.x < -5.6 && objects[0].transform.position.x > -7.7)
            {
                Destroy(objects[0].gameObject);
                objects.RemoveAt(0);
                face.GetComponent<SpriteRenderer>().sprite = faceSprite1;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite2;
                boatTimer = 0.7f - buttonSpeed;
            }
            else
            {
                face.GetComponent<SpriteRenderer>().sprite = faceSprite2;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;

                if (nextRock < 0.0f)
                {
                    index = Random.Range(0, 2);
                    Instantiate<GameObject>(rocks[index]);
                    nextRock = 4.2f;
                }
            }
        }
        if (Input.GetButtonDown("X"))
        {
            if (objects[0].name == "X(Clone)" && objects[0].transform.position.x < -5.6 && objects[0].transform.position.x > -7.7)
            {
                Destroy(objects[0].gameObject);
                objects.RemoveAt(0);
                face.GetComponent<SpriteRenderer>().sprite = faceSprite1;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite2;
                boatTimer = 0.7f - buttonSpeed;
            }
            else
            {
                face.GetComponent<SpriteRenderer>().sprite = faceSprite2;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;

                if (nextRock < 0.0f)
                {
                    index = Random.Range(0, 2);
                    Instantiate<GameObject>(rocks[index]);
                    nextRock = 4.2f;
                }
            }
        }
        if (Input.GetButtonDown("Circle"))
        {
            if (objects[0].name == "Circle(Clone)" && objects[0].transform.position.x < -5.6 && objects[0].transform.position.x > -7.7)
            {
                Destroy(objects[0].gameObject);
                objects.RemoveAt(0);
                face.GetComponent<SpriteRenderer>().sprite = faceSprite1;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite2;
                boatTimer = 0.7f - buttonSpeed;
            }
            else
            {
                face.GetComponent<SpriteRenderer>().sprite = faceSprite2;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;

                if (nextRock < 0.0f)
                {
                    index = Random.Range(0, 2);
                    Instantiate<GameObject>(rocks[index]);
                    nextRock = 4.2f;
                }
            }
        }
        if (Input.GetButtonDown("Triangle"))
        {
            if (objects[0].name == "Triangle(Clone)" && objects[0].transform.position.x < -5.6 && objects[0].transform.position.x > -7.7)
            {
                Destroy(objects[0].gameObject);
                objects.RemoveAt(0);
                face.GetComponent<SpriteRenderer>().sprite = faceSprite1;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite2;
                boatTimer = 0.7f - buttonSpeed;
            }
            else
            {
                face.GetComponent<SpriteRenderer>().sprite = faceSprite2;
                Boat.GetComponent<SpriteRenderer>().sprite = boatSprite1;

                if (nextRock < 0.0f)
                {
                    index = Random.Range(0, 2);
                    Instantiate<GameObject>(rocks[index]);
                    nextRock = 4.2f;
                }
            }
        }

    }
}

