using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {

    // Use this for initialization
    Animator myAnimator;
    public Animation myAnimation;

	void Start () {
        myAnimator = GetComponent<Animator>();
        myAnimation = GetComponent<Animation>();
        foreach(AnimationState state in myAnimation)
        {
            state.speed = 0.5F;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            myAnimator.Play(0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myAnimator.Play(0);
        }
	}
}
