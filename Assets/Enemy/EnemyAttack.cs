using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1.0f; // Time in seconds between each Attack
    public int attackDamage = 10;

    GameObject player; // reference to players
    CharacterHealth playerHealth; // reference to player's health
    bool playerInRange; //whether player is within the trigger collider and can be attacked
    float timer; // time for counting up to the next attack

    void Awake()
    {
        //Setting up the references
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<CharacterHealth>();
        //attackAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide");
        //If the entering collider is the player
        if (other.gameObject == player)
        {
            // player is in range
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //If the exiting collider is the player...
        if (other.gameObject == player)
        {
            //the player is no longer in range
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            attack();
        }
    }

    void attack()
    {
        // Reset the timer
        timer = 0f;
        playerHealth.TakeDamage(attackDamage);
        Debug.Log("Player has been attacked");
    }
}
