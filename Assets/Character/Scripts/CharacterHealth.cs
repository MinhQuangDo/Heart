using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    public Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private string curScene;
    private void Start()

    {
        health = maxHealth;

        // DontDestroyOnLoad(health);
    }

    private void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }

            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health == 0)
        {
            GameObject p = GameObject.Find("Player");
            if (!p)
            {
                p = GameObject.Find("Player_2");
            }
            animator.SetBool("Dead", true);
            PlayerMovement pm = p.GetComponent<PlayerMovement>();
            pm.alive = false;
            StartCoroutine(wait());
        }
    }

    public void TakeDamage(int damage, Vector3 position)
    {
        health -= damage;
        GameObject p = GameObject.Find("Player");
        if (!p)
        {
            p = GameObject.Find("Player_2");
        }

        //if (health == 0)
        //{
        //  animator.SetBool("Dead", true);
        //  PlayerMovement pm = p.GetComponent<PlayerMovement>();
        //  pm.alive = false;
        //  StartCoroutine(wait());
        //}

        if (health > 0)
        {

            rb = p.GetComponent<Rigidbody2D>();
            if (position.x > rb.transform.position.x)
                rb.AddForce(Vector2.left * 1500);
            //p.transform.position = Vector2.Lerp(p.transform.position, new Vector2(p.transform.position.x - 3, p.transform.position.y), 0.25f);
            else if (position.x < rb.transform.position.x)
                rb.AddForce(Vector2.right * 1500);
            animator.SetBool("Hurt", true);
            StartCoroutine(gotHurt());
        }
    }

    IEnumerator gotHurt()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hurt", false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("death", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(curScene);
    }
}
