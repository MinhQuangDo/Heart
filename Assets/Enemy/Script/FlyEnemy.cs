using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool m_FacingRight = false;
    private Vector3 m_Velocity = Vector3.zero;

    private Animator animator;
    Rigidbody2D rb;

    public float T;
    public int health = 3;
    public float speed = 10f;
    Vector2 horizontalMove;
    private float dazedTime;
    public float startDazedTime;
    float x = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        horizontalMove = new Vector2(speed, 0);
    }

    void Update()
    {
        if (dazedTime <= 0)
        {
            if (x != 0)
            {
                speed = x;
                x = 0;
            }
        }
        else
        {
            if (x == 0)
                x = speed;
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {

            StartCoroutine(dead());
        }

        T -= Time.deltaTime;
        GameObject p = GameObject.Find("Player");
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

    void Move(Vector2 move)
    {
        Vector3 targetVelocity = new Vector2(move.x*10f, move.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        GameObject p = GameObject.Find("Player");
        if (!p)
        {
            p = GameObject.Find("Player_2");
        }
        if (p.transform.position.x - 10 < rb.transform.position.x && p.transform.position.x > rb.transform.position.x)
        {
            animator.SetBool("Find", true);
            if (m_FacingRight)
                Flip();
            if (p.transform.position.y > rb.transform.position.y)
                horizontalMove = new Vector2(Mathf.Abs(horizontalMove.x), 30);
            else if (p.transform.position.y < rb.transform.position.y)
                horizontalMove = new Vector2(Mathf.Abs(horizontalMove.x), -30);
            else if (rb.transform.position.y < 5)
                horizontalMove = new Vector2(Mathf.Abs(horizontalMove.x), 30);

        }
        else if (p.transform.position.x + 10 > rb.transform.position.x && p.transform.position.x < rb.transform.position.x)
        {
            animator.SetBool("Find", true);
            if (!m_FacingRight)
                Flip();
            if (p.transform.position.y > rb.transform.position.y)
                horizontalMove = new Vector2(- Mathf.Abs(horizontalMove.x), 30);
            else if (p.transform.position.y < rb.transform.position.y)
                horizontalMove = new Vector2(- Mathf.Abs(horizontalMove.x), -30);
        }
        else if (T <= 0)
        {
            Flip();
            T = 10;
            horizontalMove = -1 * horizontalMove;
        }
        else
        {
            animator.SetBool("Find", false);
            if (rb.transform.position.y < 5)
                horizontalMove = new Vector2(horizontalMove.x, 30);
            else
                horizontalMove = new Vector2(horizontalMove.x, 0);
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
        horizontalMove = new Vector2(0, -100);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
