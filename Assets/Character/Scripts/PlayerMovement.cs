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
    public bool alive = true;

    void Update()
    {
      if (alive)
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
      else
      {
        horizontalMove = 0;
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

    public void Die()
    {
        // Set alive to false
        alive = false;
        // rb.velocity = Vector2.zero;
        transform.Rotate(new Vector3(0, 0, 90), Space.Self);
        // deathNoise.Play();
        // Velicity to zero
    }


    public void Respawn()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        alive = true;

    }
}
