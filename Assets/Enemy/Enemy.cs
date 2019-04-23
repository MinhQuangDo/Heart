using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool m_FacingRight = false;
    private Vector3 m_Velocity = Vector3.zero;

    private Animator animator;
    Rigidbody2D rb;

    public int health = 3;
    public float speed = 10f;
    float horizontalMove = 0f;
    private float dazedTime;
    public float startDazedTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 10;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {

            StartCoroutine(dead());
        }

        horizontalMove = -1 * speed;
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("Hurt", true);
        dazedTime = startDazedTime;
        health -= damage;
        StartCoroutine(gotHurt());
    }

    void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
