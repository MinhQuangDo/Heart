using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40.0f;
    float horizontalMove = 0f;
    bool jump = false;
    bool jumpFlag = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            StartCoroutine(jumpAnimation());
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);

        jump = false;
    }

    IEnumerator jumpAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("IsJumping", false);
    }
}
