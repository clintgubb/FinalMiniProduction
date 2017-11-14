using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.T))
        {
            print("Test");
        }

    }

   public void OnMouseDown()
    {
      print("Start button working");
      SceneManager.LoadScene("Ocean");
    }
}
