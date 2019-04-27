using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEnemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool m_FacingRight = false;
    private Vector3 m_Velocity = Vector3.zero;

    private Animator animator;
    Rigidbody2D rb;

    public float T;
    public int health = 3;
    public float speed = 10f;
    float horizontalMove = 0f;
    private float dazedTime;
    public float startDazedTime;

    public int att = 0;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        horizontalMove = -1 * speed;
        Flip();
    }

    void Update()
    {
        if (dazedTime <= 0)
        {
            speed = 10;
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

        T -= Time.deltaTime;

        GameObject p = GameObject.Find("Player");
        if(!p)
        {
            p = GameObject.Find("Player_2");
        }

        if(timeBtwAttack <= 0)
        {
            if (p.transform.position.x - 1 < rb.transform.position.x && p.transform.position.x > rb.transform.position.x)
            {
                StartCoroutine(attack());
            }
            else if (p.transform.position.x + 1 > rb.transform.position.x && p.transform.position.x < rb.transform.position.x)
            {
                StartCoroutine(attack());
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
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

        GameObject p = GameObject.Find("Player");
        if (!p)
        {
            p = GameObject.Find("Player_2");
        }
        if (p.transform.position.x - 10 < rb.transform.position.x && p.transform.position.x > rb.transform.position.x)
        {
            if (m_FacingRight)
            {
                Flip();
                horizontalMove = Mathf.Abs(horizontalMove);
            }
        }
        else if (p.transform.position.x + 10 > rb.transform.position.x && p.transform.position.x < rb.transform.position.x)
        {
            if (!m_FacingRight)
            {
                Flip();
                horizontalMove = -1 * Mathf.Abs(horizontalMove);
            }
        }
        else if (T <= 0)
        {
            Flip();
            T = 10;
            horizontalMove = -1 * horizontalMove;
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

    IEnumerator attack()
    {
        animator.SetBool("Attack", true);
        att = 1;
        yield return new WaitForSeconds(0f);
        att = 0;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Attack", false);
    }

    IEnumerator dead()
    {
        animator.SetBool("Dead", true);
        horizontalMove = 0;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
