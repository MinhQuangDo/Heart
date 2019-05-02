using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy_Boss : MonoBehaviour
{
    Rigidbody2D rb;

    private float health;
    public float maxHealth;
    private float timeBtwShot;
    public float startTimeBtwShot;

    public GameObject projectile;
    public Transform shotPoint;
    private Animator animator;
    public int dif;
    GameObject player;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    bool dead;
    public GameObject boss2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");

        timeBtwShot = -1;
        health = maxHealth;
    }

    private void Update()
    {
        rb.velocity = new Vector2(0, 0);

        if(health <= 1)
        {
            dead = true;
        }


        if (health <= 0)
        {
          for (int i = 0; i < hearts.Length; i++)
          {
            hearts[i].sprite = emptyHearts;
          }
            animator.SetBool("Dead", true);
            if(GameObject.Find("Boss"))
                StartCoroutine(boss1End());
            if(GameObject.Find("Boss2"))
                StartCoroutine(boss2End());
            // Debug.Log("lol");
        }
        else
        {
            if (timeBtwShot < 0)
            {
                Vector3 diff = player.transform.position - transform.position;
                float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                Instantiate(projectile, shotPoint.position, Quaternion.Euler(0, 0, rotZ));
                timeBtwShot = startTimeBtwShot;
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHearts;
                }
                else
                {
                    hearts[i].sprite = emptyHearts;
                }

                if (i < maxHealth)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= 1;

        if (!dead)
        {

            int where = Random.Range(0, 8);

            switch (where)
            {
                case 0:
                    transform.position = new Vector3(14.5f, 4.1f, 0);
                    break;
                case 1:
                    transform.position = new Vector3(8f, 7.1f, 0);
                    break;
                case 2:
                    transform.position = new Vector3(1.4f, 10.1f, 0);
                    break;
                case 3:
                    transform.position = new Vector3(22f, 6.1f, 0);
                    break;
                case 4:
                    transform.position = new Vector3(8f, 13.1f, 0);
                    break;
                case 5:
                    transform.position = new Vector3(16f, 11.1f, 0);
                    break;
                case 6:
                    transform.position = new Vector3(23f, 14.1f, 0);
                    break;
                case 7:
                    transform.position = new Vector3(29f, 9.1f, 0);
                    break;
                case 8:
                    transform.position = new Vector3(16f, 0.1f, 0);
                    break;
            }
        }
    }

    IEnumerator boss1End()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        boss2.SetActive(true);
    }

    IEnumerator boss2End()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("destroyed");
        Destroy(gameObject);
        SceneManager.LoadScene("Closing");

    }
}
