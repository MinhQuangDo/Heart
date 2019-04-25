using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

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
                
                StartCoroutine(attacking());

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);

                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Enemy e = enemiesToDamage[i].GetComponent<Enemy>();
                    if (e)
                    {
                        
                        CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                        e.TakeDamage(damage);
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
