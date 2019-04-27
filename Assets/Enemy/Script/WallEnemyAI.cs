using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemyAI : MonoBehaviour
{
    public Transform originPoint;
    private Vector2 dir = new Vector2(-1, 0);
    public float range = 1f;
    public float speed = -2f;

    public Animator animator;

    public int health = 3;
    private float dazedTime;
    public float startDazedTime;
    private float tempSpeed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.DrawRay(originPoint.position, dir * range);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, dir, range);
        if (hit == true)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Flip();
                speed *= -1;
                dir *= -1; 
            }
        }

        if (dazedTime <= 0)
        {
            if (dir == new Vector2(-1, 0))
                speed = -2;
            else if (dir == new Vector2(1, 0))
                speed = 2;
        }
        else
        {
            speed = 0;

            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {

            StartCoroutine(dead());
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

    public void TakeDamage(int damage)
    {
        animator.SetBool("Hurt", true);
        dazedTime = startDazedTime;
        health -= damage;
        StartCoroutine(gotHurt());
    }

    IEnumerator gotHurt()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hurt", false);
    }

    IEnumerator dead()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
