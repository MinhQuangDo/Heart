using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public AudioSource audioSource;
    public AudioClip collisionSoundClip;
    public Transform attackPos;
    public LayerMask WhatIsEnemies;
    public float attackRange;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetButtonDown("Attack"))
            {
                animator.SetBool("Sword", true);
                audioSource.PlayOneShot(collisionSoundClip);

                StartCoroutine(attacking());

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);

                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    WallEnemyAI e = enemiesToDamage[i].GetComponent<WallEnemyAI>();
                    NEnemy e2 = enemiesToDamage[i].GetComponent<NEnemy>();
                    FlyEnemy e3 = enemiesToDamage[i].GetComponent<FlyEnemy>();
                    Enemy_Boss eb = enemiesToDamage[i].GetComponent<Enemy_Boss>();

                    if (e)
                    {
                        CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                        e.TakeDamage(damage);
                    }
                    if (e2)
                    {
                        CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                        e2.TakeDamage(damage);
                    }
                    if (e3)
                    {
                        CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                        e3.TakeDamage(damage);
                    }
                    if(eb)
                    {
                        CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                        eb.TakeDamage(damage);
                    }
                }

                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    IEnumerator attacking()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Sword", false);
    }
}
