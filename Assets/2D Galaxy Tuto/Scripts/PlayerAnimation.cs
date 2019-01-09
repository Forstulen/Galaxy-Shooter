using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _Animator;

    // Use this for initialization
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        float movement = CrossPlatformInputManager.GetAxis("Horizontal");

        if ( movement < 0.0f) {
            _Animator.SetBool("Turn_Left", true);
            _Animator.SetBool("Turn_Right", false);
        } else if ( movement > 0.0f) {
            _Animator.SetBool("Turn_Left", false);
            _Animator.SetBool("Turn_Right", true);
        } else {
            _Animator.SetBool("Turn_Left", false);
            _Animator.SetBool("Turn_Right", false);
        }


#else
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _Animator.SetBool("Turn_Left", true);
            _Animator.SetBool("Turn_Right", false);
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _Animator.SetBool("Turn_Right", true);
            _Animator.SetBool("Turn_Left", false);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _Animator.SetBool("Turn_Left", false);
            _Animator.SetBool("Turn_Right", false);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _Animator.SetBool("Turn_Left", false);
            _Animator.SetBool("Turn_Right", false);
        }
#endif
       


            
	}
}
