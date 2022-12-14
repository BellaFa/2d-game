/* Helper Function that constanly checks for movement updates */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public Controller2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
 

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump")){
           jump = true;
           animator.SetBool("isJumping", true);
         }
        
        fixedUpdate();
    }
    
    public void onLanding ()    {
        animator.SetBool("isJumping",false);
    }


    void fixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime,crouch, jump);
        jump = false;
    }
}
