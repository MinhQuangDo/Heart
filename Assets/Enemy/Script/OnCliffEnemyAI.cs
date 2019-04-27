using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCliffEnemyAI : MonoBehaviour
{
    public Transform originPoint;
    private Vector2 dir = new Vector2(-1, 0);
    public float range = 0.01f;
    public float speed = -2f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.DrawRay(originPoint.position, dir * range);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, dir, range);
        //if (hit == true)
        //{
        //if (hit.collider.CompareTag("Ground"))
        //{
        //    Flip();
        //    speed *= -1;
        //    dir *= -1; 
        //}
        //}

        if (hit == false || hit.collider.CompareTag("Player"))
        {
            Flip();
            speed *= -1;
            dir *= -1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
